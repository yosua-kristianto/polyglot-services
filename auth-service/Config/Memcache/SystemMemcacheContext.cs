using StackExchange.Redis;

namespace AuthService.Config.Memcache;

public interface IRedisConnection
{
    IDatabase Database { get; }
}

public class RedisOptions
{
    public string Host {get;set;}
    public int Port {get;set;}
    public string Password {get;set;}
}

public class SystemCacheUMAContext : IRedisConnection
{
    private readonly Lazy<ConnectionMultiplexer> _connection;

    public SystemCacheUMAContext(IConfiguration configuration)
    {
        
        var redisConnectionString = configuration.GetConnectionString("SysCache") ?? "localhost:6739";
        this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConnectionString));
    }

    public IDatabase Database => this._connection.Value.GetDatabase();
}