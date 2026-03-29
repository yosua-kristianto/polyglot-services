using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemConfigurator.Config.Database.Seeder.NMA;
using SystemConfigurator.Config;
using SystemConfigurator.Config.Database;
using SystemConfigurator.Config.Database.Seeder.UMA;

namespace SystemConfigurator;

class Program
{
    /// <summary>
    /// This function is boilerplate of this project. Used for printing micro service banner
    /// to easily identify everything running service.
    /// </summary>
    static void PrintBanner()
    {
        // Read the file as embedded resources
        var assembly = Assembly.GetExecutingAssembly();
        string resourceName = "SystemConfigurator.SysConfigServices.banner.txt";

        using Stream stream = assembly.GetManifestResourceStream(resourceName);

        if(stream == null)
        {
            Console.WriteLine(":: McAstr SysConfig Services ::");
        }else
        {
            using StreamReader reader = new StreamReader(stream);
            Console.WriteLine(reader.ReadToEnd());
        }
    }

    #region env

    /// <summary>
    /// This function is boilerplate of this project. Used for determine which environment 
    /// variable used during run time.
    /// </summary>
    static IHost EnvironmentGatherer(string environment)
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
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostCtx, config) =>
            {
                var env = hostCtx.HostingEnvironment;

                config.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

                config.AddEnvironmentVariables();
            })
            .ConfigureServices((ctx, services) =>
            {
                var config = ctx.Configuration;

                services.AddDbContext<SystemDbUMAContext>(options =>
                {
                    options.UseNpgsql(
                        config.GetConnectionString("UMAConnection")
                    );
                });

                services.AddDbContext<SystemDbNMAContext>(options =>
                {
                    options.UseNpgsql(
                        config.GetConnectionString("NMAConnection")
                    );
                });

                services.AddDbContext<SystemDbCMAContext>(options =>
                {
                    options.UseNpgsql(
                        config.GetConnectionString("CMAConnection")
                    );
                });
            })
            .Build();

        host.RunAsync();

        return host;
    }

    #endregion

    #region database stuffs

    /// <summary>
    /// This function handle Database Migration. 
    /// </summary>
    /// <param name="host"></param>
    static void DatabaseMigrationRunner(IHost host)
    {
        Console.WriteLine("Starting database migration...");
        using IServiceScope scope = host.Services.CreateScope();
        // Migrate UMA Database.
        var db = scope.ServiceProvider.GetRequiredService<SystemDbUMAContext>();
        db.Database.Migrate();
        Console.WriteLine("Database UMA migration completed.");

        // Migrate NMA Database.
        var dbNma = scope.ServiceProvider.GetRequiredService<SystemDbNMAContext>();
        dbNma.Database.Migrate();
        Console.WriteLine("Database NMA migration completed.");

        // Migrate CMA Database.
        var dbCma = scope.ServiceProvider.GetRequiredService<SystemDbCMAContext>();
        dbCma.Database.Migrate();
        Console.WriteLine("Database CMA migration completed.");
    }

    /// <summary>
    /// This function handle Database Seeder.
    /// 
    /// If the passed env is "prod", then it only run the "Mandatory Seeder".
    /// </summary>
    /// <param name="host"></param>
    /// <param name="env"></param>
    static void DatabaseSeederController(IHost host, string env)
    {
        using IServiceScope scope = host.Services.CreateScope();

        // Seed Mandatory
        var db = scope.ServiceProvider.GetRequiredService<SystemDbUMAContext>();
        var dbNma = scope.ServiceProvider.GetRequiredService<SystemDbNMAContext>();
        new MandatorySeeder(db).Seed().GetAwaiter();
        new PersonalNotificationSeeder(dbNma).Seed().GetAwaiter();

        if (env != "prod")
        {
            // Seed Users for each Role except Sysadmin
        }
    }

    #endregion

    static void Main(string[] args)
    {
        // Arguments collections
        // Determine env's arguments. Must be added on start.
        string? envArg = args.FirstOrDefault(arg => arg.StartsWith("--env", StringComparison.OrdinalIgnoreCase));

        if(envArg == null)
        {
            ConsoleHelper.WriteLineWarning("No --env argument found on run. Treating this running session as \"local\".");
        }

        string environment = envArg?.Split('=')[1] ?? "local";

        IHost envHost = EnvironmentGatherer(environment);

        PrintBanner();

        Console.WriteLine($"\nStarted at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
        Console.WriteLine($"Environment: {environment}");

        // Determine whether migration is runned or not.
        string? migrationArg = args.FirstOrDefault(arg => arg.StartsWith("--migrate", StringComparison.OrdinalIgnoreCase));

        if (migrationArg != null)
        {
            Console.WriteLine("Migration argument found. Running migration....");
            DatabaseMigrationRunner(envHost);
        }

        // Seeder Control
        string? seederArg = args.FirstOrDefault(arg => arg.StartsWith("--seed", StringComparison.OrdinalIgnoreCase));

        if (seederArg != null)
        {
            Console.WriteLine("Seeder argument found. Running database seeder....");

            DatabaseSeederController(envHost, envArg);
        }
    }
}