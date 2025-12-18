using System.Reflection;
using System.Text.Json;
using auth_service.Api.Handler;
using auth_service.Config.Kafka;
using auth_service.Messaging.Producer;
using auth_service.Repository.Memcache.Otp;
using auth_service.Repository.Memcache.Token;
using auth_service.Repository.UMA;
using AuthService.Api.Handler;
using AuthService.Config;
using AuthService.Config.Database;
using AuthService.Config.Memcache;
using AuthService.Repository.Memcache.Token;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
namespace AuthService;

public class Program
{
    /// <summary>
    /// This function is boilerplate of this project. Used for printing micro service banner
    /// to easily identify everything running service.
    /// </summary>
    static void PrintBanner()
    {
        // Read the file as embedded resources
        var assembly = Assembly.GetExecutingAssembly();
        string resourceName = "AuthService.banner.txt";
        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        if(stream == null)
        {
            Console.WriteLine(":: McAstr UMA Services ::");
        }
        else
        {
            using StreamReader reader = new StreamReader(stream);
            Console.WriteLine(reader.ReadToEnd());
        }
    }

    /// <summary>
    /// This function is boilerplate of this project. Used for determine which environment 
    /// variable used during run time.
    /// </summary>
    static WebApplicationBuilder EnvironmentGatherer(string environment, WebApplicationBuilder builder)
    {
        // Valid env
        string[] validEnvArgs = new[] { "local", "dev", "prod" };
        
        if (!validEnvArgs.Contains(environment))
        {
            string exceptionString = $"The only valid env args are either \"local\", \"dev\", or \"prod\" found \"{environment}\"";
            ConsoleHelper.WriteLineError(exceptionString);
            throw new Exception(exceptionString);
        }

        // Builder
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        return builder;
    }

    static void PersistanceConnection(WebApplicationBuilder builder)
    {
        // Entity Framework Core - Database
        builder.Services.AddDbContext<SystemDbUMAContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("UMAConnection"))
        );

        // Stack Exchange Redis
        builder.Services.Configure<RedisOptions>(
            builder.Configuration.GetSection("Redis")
        );
        builder.Services.AddSingleton<IRedisConnection, SystemCacheUMAContext>();

        builder.Services.AddSingleton<KafkaContext>();
    }

    public static void RegisterDI(WebApplicationBuilder builder)
    {
        // Add Dependency Injection registrations here
        builder.Services.AddScoped<IAuthControllerHandler, AuthControllerHandler>();
        builder.Services.AddScoped<IUMARepository, UMARepository>();
        builder.Services.AddScoped<IOtpRepository, OtpRepository>();
        builder.Services.AddScoped<ITokenCacheRepository, TokenCacheRepository>();

        builder.Services.AddScoped<AuthenticationSMTPMessageProducer>();
    }

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Arguments collections
        // Determine env's arguments. Must be added on start.
        string? envArg = args.FirstOrDefault(arg => arg.StartsWith("--env", StringComparison.OrdinalIgnoreCase));

        if(envArg == null)
        {
            ConsoleHelper.WriteLineWarning("No --env argument found on run. Treating this running session as \"local\".");
        }

        string environment = envArg?.Split('=')[1] ?? "local";

        WebApplicationBuilder envHost = EnvironmentGatherer(environment, builder);

        PrintBanner();

        PersistanceConnection(envHost);

        RegisterDI(builder);

        // Add services to the container.
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy =
                    JsonNamingPolicy.SnakeCaseLower;

                options.JsonSerializerOptions.DictionaryKeyPolicy =
                    JsonNamingPolicy.SnakeCaseLower;
            });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}