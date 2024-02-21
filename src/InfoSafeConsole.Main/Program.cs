using InfoSafeConsole.Application;
using InfoSafeConsole.Application.Interfaces;
using InfoSafeConsole.Main;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Serilog;

var configuration = BuildConfig()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

Log.Information("Starting up");

try
{
    using IHost host = CreateHostBuilder(args)
        .UseSerilog()
        .Build();

    //IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
    //var val = config.GetConnectionString("DBConnectionString");

    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    services.GetRequiredService<App>().Run(args);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

IHostBuilder CreateHostBuilder(string[] args)
{
    var builder = Host.CreateDefaultBuilder(args);

    builder.ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IInfoSafeService, InfoSafeService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5019");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        services.AddSingleton<IAppService, AppService>();
        services.AddSingleton<App>();
    });

    return builder;
}

IConfigurationBuilder BuildConfig()
{
    var builder = new ConfigurationBuilder();

    builder
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();

    return builder;
}