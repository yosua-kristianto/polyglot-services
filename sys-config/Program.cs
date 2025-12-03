using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemConfigurator.Config;

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
                    options.UseNpgsql(config.GetConnectionString("UMAConnection"));
                });
            })
            .Build();

        host.RunAsync();

        return host;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}