using AuthService.Config.Memcache;
using StackExchange.Redis;

namespace AuthService.Repository.Memcache;

public abstract class BaseMemcacheRepository
{
    protected readonly IDatabase _redisConnection;
    protected readonly IServer _redisServer;
    public BaseMemcacheRepository(IRedisConnection redisConnection) 
    { 
        this._redisConnection = redisConnection.Database; 
        this._redisServer = redisConnection.Server;
    }
}