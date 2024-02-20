using Microsoft.Extensions.Logging;

namespace InfoSafeConsole.Application
{
    public class AppService : IAppService
    {
        private readonly ILogger<AppService> _logger;

        public AppService(
            ILogger<AppService> logger)
        {
            _logger = logger;
        }

        public void CalculateCustomerAge(int id)
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(AppService), nameof(CalculateCustomerAge));

            Console.WriteLine("CustomerService:CalculateCustomerAge runs");
        }
    }
}