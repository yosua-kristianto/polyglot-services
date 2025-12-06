using System;

namespace AuthService.Model.Memcache;

public class TokenCache
{
    public Guid TokenId { get; set; }
    public Guid RefreshId { get; set; }
    public Guid UserId { get; set; }
    public Guid DeviceId { get; set; }
    public string Token { get; set; }
    public long CreatedAt { get; set; }
}
