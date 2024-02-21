using InfoSafeConsole.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace InfoSafeConsole.Application
{
    public class AppService : IAppService
    {
        private readonly ILogger<AppService> _logger;
        private readonly IInfoSafeService _infoSafeService;

        public AppService(
            ILogger<AppService> logger,
            IInfoSafeService infoSafeService)
        {
            _logger = logger;
            _infoSafeService = infoSafeService;
        }

        public async Task<int> CalculateCustomerAgeAsync(int id)
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(AppService), nameof(CalculateCustomerAgeAsync));

            var retval = await Task.Run(() => id);
            return retval;
        }
    }
}