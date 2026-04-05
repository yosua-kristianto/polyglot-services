using AuthService.Model.Entity;
using System;

namespace AuthService.Model.Memcache;

public class TokenCache
{
    public const int TOKEN_EXPIRITY_IN_MINUTES = 120;

    public const string TableName = "uma_cache_token";
    public Guid TokenId { get; set; }
    public Guid RefreshId { get; set; }
    public Guid UserId { get; set; }
    public Guid DeviceId { get; set; }
    public User Identity { get; set; }
    public long CreatedAt { get; set; }

    /// <summary>
    /// 
    /// Since April, 5th 2026. Addition ExpiredAt to invoke TTL
    /// 
    /// </summary>
    public long ExpiredAt { get; set; }
}
