using System;

namespace AuthService.Model.Memcache;

public class OtpCache
{
    public Guid UserId { get; set; }
    public string Otp { get; set; }
    public long CreatedAt { get; set; }
    public long ExpiredAt { get; set; }
}
