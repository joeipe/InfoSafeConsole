using InfoSafeConsole.Application;
using InfoSafeConsole.Application.Interfaces;
using InfoSafeConsole.Main;
using InfoSafeConsole.Main.HttpHandlers;
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

    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    await services.GetRequiredService<App>().RunAsync(args);
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
        services.AddScoped<BearerTokenHandler>();
        services.AddSingleton<IAppService, AppService>();
        services.AddSingleton<App>();

        services.AddHttpClient<IInfoSafeService, InfoSafeService>(client =>
        {
            client.BaseAddress = new Uri(context.Configuration.GetValue<string>("ApiClient:InfoSafeUri"));
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        })
            .AddHttpMessageHandler<BearerTokenHandler>();
    });

    return builder;
}

IConfigurationBuilder BuildConfig()
{
    var builder = new ConfigurationBuilder();

    builder
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder;
}