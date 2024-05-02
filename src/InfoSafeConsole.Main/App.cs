using InfoSafeConsole.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using SharedKernel.Extensions;
using System.Threading.Tasks;

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

            /*
            //Task 1
            var age = await _appService.CalculateCustomerAgeAsync(1);
            _logger.LogInformation($"Customer Age is {age.OutputJson()}");

            //Task 2
            var contacts = await _infoSafeService.GetFullContactsAsync();
            _logger.LogInformation($"Full Contacts \n{contacts.OutputJson()}" );
            */

            //Task 1
            var param = 1;
            var calculateCustomerAgeTask = _appService.CalculateCustomerAgeAsync(param).ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    _logger.LogInformation($"Customer Age({param}) is {t.Result.OutputJson()}");
                }
                if (t.Status == TaskStatus.Faulted)
                {
                    var ex = t.Exception;
                    _logger.LogError(t.Exception, $"An error occurred in {nameof(_appService)}.{nameof(_appService.CalculateCustomerAgeAsync)}: {ex?.Message} ");
                }
            });

            //Task 2
            var getFullContactsTask = _infoSafeService.GetFullContactsAsync().ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    _logger.LogInformation($"Full Contacts \n{t.Result.OutputJson()}");
                }
                if (t.Status == TaskStatus.Faulted)
                {
                    var ex = t.Exception;
                    _logger.LogError(t.Exception, $"An error occurred in {nameof(_infoSafeService)} {nameof(_infoSafeService.GetFullContactsAsync)}: {ex?.Message} ");
                }
            });

            await Task.WhenAll(
                calculateCustomerAgeTask,
                getFullContactsTask
            );

            _logger.LogInformation("{Class}.{Action} stop", nameof(App), nameof(RunAsync));
        }
    }
}