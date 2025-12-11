using System;
using System.Text.Json;
using AuthService.Config.Memcache;
using AuthService.Model.Memcache;
using AuthService.Repository.Memcache;
using StackExchange.Redis;

namespace auth_service.Repository.Memcache.Otp;

public class OtpRepository(IRedisConnection ctx) : BaseMemcacheRepository(ctx), IOtpRepository
{
    public async Task CreateOtp(OtpCache otpInstance)
    {
        string key = $"{OtpCache.TableName}:{otpInstance.UserId}";
        string data = JsonSerializer.Serialize(otpInstance);

        await this._redisConnection.StringSetAsync(key, data);
    }

    public async Task DistinguishOtpByUserId(Guid userId)
    {
        string key = $"{OtpCache.TableName}:{userId}";
        await this._redisConnection.KeyDeleteAsync(key);
    }

    public async Task<OtpCache?> GetOtpByUserId(Guid userId)
    {
        string key = $"{OtpCache.TableName}:{userId}";
        string? data = await this._redisConnection.StringGetAsync(key);

        if (data == null)
        {
            return null;
        }
        
        OtpCache otp = JsonSerializer.Deserialize<OtpCache>(data)!;

        return otp;
    }
}
