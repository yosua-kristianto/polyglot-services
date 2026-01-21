using AuthService.Common.Exceptions;
using AuthService.Messaging.Producer;
using AuthService.Repository.Memcache.Otp;
using AuthService.Repository.UMA;
using AuthService.model.Object.Response;
using AuthService.Model.Entity;
using AuthService.Model.Memcache;
using AuthService.Model.Object.Messaging.Producer;
using AuthService.Repository.Memcache.Token;

namespace AuthService.Api.Handler;

public class AuthControllerHandler : IAuthControllerHandler
{
    private readonly IUMARepository _umaRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly ITokenCacheRepository _tokenCacheRepository;

    private readonly IAuthenticationSMTPMessageProducer _authOtpProducer;

    public AuthControllerHandler(IUMARepository umaRepository, IOtpRepository otpRepository, ITokenCacheRepository tokenCacheRepository, IAuthenticationSMTPMessageProducer authOtp)
    {
        this._umaRepository = umaRepository;
        this._otpRepository = otpRepository;
        this._tokenCacheRepository = tokenCacheRepository;
        this._authOtpProducer = authOtp;
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

        OtpCache otp = new OtpCache
        {
            UserId = existingUser.Id,
            Otp = GenerateOTPCode(),
            ExpiredAt = DateTimeOffset.UtcNow.AddMinutes(2).ToUnixTimeMilliseconds()
        };

        this._otpRepository.CreateOtp(otp);

        // @TODO Send to NMA

        AuthenticationOTPMessageProducerDTO producerDTO = new()
        {
            UserId = existingUser.Id,
            Email = existingUser.Email,
            Otp = otp.Otp,
            RecipientName = existingUser.Name
        };

        this._authOtpProducer.Send(producerDTO).GetAwaiter();
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

        AuthenticationOTPMessageProducerDTO producerDTO = new()
        {
            UserId = user.Id,
            Email = user.Email,
            Otp = otp.Otp,
            RecipientName = user.Name
        };

        this._authOtpProducer.Send(producerDTO).GetAwaiter();
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
