using StackExchange.Redis;

namespace AuthService.Config.Memcache;

public interface IRedisConnection
{
    IDatabase Database { get; }
    IServer Server { get; }
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
    private readonly string _host;
    private readonly int _port;

    public SystemCacheUMAContext(IConfiguration configuration)
    {
        
        var redisConnectionString = configuration.GetConnectionString("SysCache") ?? "localhost:6739";
        Console.WriteLine($"Connecting to Redis at {redisConnectionString}");

        // Parse host and port so GetServer can use them
        var parts = redisConnectionString.Split(',')[0].Split(":");
        _host = parts[0];
        _port = int.Parse(parts[1]);

        this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConnectionString));
    }

    public IDatabase Database => this._connection.Value.GetDatabase();

    public IServer Server => _connection.Value.GetServer(_host, _port);
}