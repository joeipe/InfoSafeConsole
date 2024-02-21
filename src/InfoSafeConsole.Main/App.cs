using InfoSafeConsole.Application;
using InfoSafeConsole.Application.Interfaces;
using Microsoft.Extensions.Logging;
using SharedKernel.Extensions;

namespace InfoSafeConsole.Main
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly IAppService _appService;
        private readonly IInfoSafeService _infoSafeService;

        public App(
            ILogger<App> logger,
            IAppService appService, 
            IInfoSafeService infoSafeService)
        {
            _logger = logger;
            _appService = appService;
            _infoSafeService = infoSafeService;
        }

        public async Task RunAsync(string[] args)
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(App), nameof(RunAsync));

            //Task 1
            _appService.CalculateCustomerAge(1);

            //Task 2
            var contacts = await _infoSafeService.GetFullContactsAsync();
            _logger.LogInformation(contacts.OutputJson());

            _logger.LogInformation("{Class}.{Action} stop", nameof(App), nameof(RunAsync));
        }
    }
}