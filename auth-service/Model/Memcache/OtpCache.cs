using System;

namespace AuthService.Model.Memcache;

public class OtpCache
{
    public const string TableName = "uma_cache_otp";

    public Guid UserId { get; set; }
    public string Otp { get; set; }
    public long CreatedAt { get; set; }
    public long ExpiredAt { get; set; }
}
