using StackExchange.Redis;

namespace AuthService.Config.Memcache;

public interface IRedisConnection
{
    IDatabase Database { get; }
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