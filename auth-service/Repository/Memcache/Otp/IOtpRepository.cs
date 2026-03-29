using System;
using AuthService.Model.Memcache;

namespace AuthService.Repository.Memcache.Otp;

public interface IOtpRepository
{
    /// <summary>
    /// Register new OTP data to Redis. 
    /// </summary>
    /// <param name="otpInstance"></param>
    /// <returns></returns>
    public Task CreateOtp(OtpCache otpInstance);

    /// <summary>
    /// Distinguish an OTP by user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task DistinguishOtpByUserId(Guid userId);

    /// <summary>
    /// Get OTP data by its identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns>WARNING: THis function may return null if identifier is not found.</returns>
    public Task<OtpCache?> GetOtpByUserId(Guid identifier);
}
