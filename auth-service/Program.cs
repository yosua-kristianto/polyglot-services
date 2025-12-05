using System.Reflection;
using Model.Object;
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

    public static void Main(string[] args)
    {
        PrintBanner();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.Run();
    }
}