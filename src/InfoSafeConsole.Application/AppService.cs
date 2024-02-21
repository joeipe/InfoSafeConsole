using InfoSafeConsole.Application.Interfaces;
using Microsoft.Extensions.Logging;

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

        public void CalculateCustomerAge(int id)
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(AppService), nameof(CalculateCustomerAge));

            Console.WriteLine("CustomerService:CalculateCustomerAge runs");
        }
    }
}