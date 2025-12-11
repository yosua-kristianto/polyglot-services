using System;
using auth_service.Common.Exceptions;
using auth_service.Repository.Memcache.Otp;
using auth_service.Repository.UMA;
using AuthService.Api.Handler;
using AuthService.Common.Exceptions;
using AuthService.model.Object.Response;
using AuthService.Model.Entity;
using AuthService.Model.Memcache;
using AuthService.Repository.Memcache.Token;

namespace auth_service.Api.Handler;

public class AuthControllerHandler : IAuthControllerHandler
{
    private readonly IUMARepository _umaRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly ITokenCacheRepository _tokenCacheRepository;

    public AuthControllerHandler(IUMARepository umaRepository, IOtpRepository otpRepository, ITokenCacheRepository tokenCacheRepository)
    {
        this._umaRepository = umaRepository;
        this._otpRepository = otpRepository;
        this._tokenCacheRepository = tokenCacheRepository;
    }

    private string GenerateOTPCode(int length = 8)
    {
        const string digits = "0123456789";
        var otp = new char[length];

        for (int i = 0; i < length; i++)
        {
            otp[i] = digits[new Random().Next(digits.Length)];
        }

        return new string(otp);
    }

    public void InvokeOTP(string email)
    {
        User? existingUser = this._umaRepository.FindUserByEmail(email) ?? throw new UserNotFoundException();

        switch (existingUser.Status)
        {
            case User.STATUS_INACTIVE:
                throw new UserNotActivatedException();
            case User.STATUS_SUSPENDED:
                throw new UserSuspendedException();
            default:
                break;
        }

        this._otpRepository.CreateOtp(new OtpCache
        {
            UserId = existingUser.Id,
            Otp = GenerateOTPCode(),
            ExpiredAt = DateTimeOffset.UtcNow.AddMinutes(2).ToUnixTimeMilliseconds()
        });

        // @TODO Send to NMA
    }

    public LoginResponseDTO Login(string email, string otp, string deviceId)
    {
        User? existingUser = this._umaRepository.FindUserByEmail(email) ?? throw new UserNotFoundException();

        switch (existingUser.Status)
        {
            case User.STATUS_INACTIVE:
                throw new UserNotActivatedException();
            case User.STATUS_SUSPENDED:
                throw new UserSuspendedException();
            default:
                break;
        }

        OtpCache? otpData = this._otpRepository.GetOtpByUserId(existingUser.Id).GetAwaiter().GetResult() ?? throw new UnauthorizedException();

        if (otpData.Otp != otp)
        {
            throw new UnauthorizedException();
        }

        TokenCache token = new TokenCache
        {
            TokenId = Guid.NewGuid(),
            RefreshId = Guid.NewGuid(),
            UserId = existingUser.Id,
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        this._tokenCacheRepository.CreateToken(token).GetAwaiter().GetResult();

        return new LoginResponseDTO
        {
            AccessToken = token.TokenId.ToString(),
            RefreshToken = token.RefreshId.ToString()
        };
    }

    public LoginResponseDTO RefreshOTP(string email)
    {
        throw new NotImplementedException();
    }

    public void Register(string email, string name)
    {
        User? existingUser = this._umaRepository.FindUserByEmail(email);

        if(existingUser != null)
        {
            throw new DuplicatedUserException();
        }

        User user = new User();
        user.Email = email;
        user.Status = User.STATUS_INACTIVE;
        user.Name = name;

        user = this._umaRepository.CreateUser(user);

        // Create OTP
        OtpCache otp = new OtpCache
        {
            UserId = user.Id,
            Otp = GenerateOTPCode(),
            ExpiredAt = DateTimeOffset.UtcNow.AddMinutes(2).ToUnixTimeMilliseconds()
        };

        this._otpRepository.CreateOtp(otp);

        // @TODO: Send email(NMA Service)
    }

    public LoginResponseDTO VerifyEmailRegistration(string email, string otp, string deviceId)
    {
        User existingUser = this._umaRepository.FindUserByEmail(email) ?? throw new UserNotFoundException();
        
        if(existingUser.Status != User.STATUS_INACTIVE)
        {
            throw new UserNotFoundException();
        }

        OtpCache? otpData = this._otpRepository.GetOtpByUserId(existingUser.Id).GetAwaiter().GetResult() ?? throw new UnauthorizedException();
        if (otpData.Otp != otp)
        {
            throw new UnauthorizedException();
        }

        this._umaRepository.updateUserStatus(existingUser.Id, User.STATUS_ACTIVE);

        return this.Login(email, otp, "");
    }
}
