using StackExchange.Redis;

namespace AuthService.Repository.Memcache;

public abstract class BaseMemcacheRepository
{
    protected readonly IConnectionMultiplexer _redisConnection;
    public BaseMemcacheRepository(IConnectionMultiplexer redisConnection) { this._redisConnection = redisConnection; }
}