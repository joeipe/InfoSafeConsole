using InfoSafeConsole.Application;
using InfoSafeConsole.Main;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

//var builder = new ConfigurationBuilder();
//BuildConfig(builder);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
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


IHostBuilder CreateHostBuilder(string[] strings)
{
    var builder = Host.CreateDefaultBuilder();

    builder.ConfigureServices((context, services) =>
    {
        services.AddSingleton<IAppService, AppService>();
        services.AddSingleton<App>();
    });

    return builder;
}

void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();
}