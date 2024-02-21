using InfoSafeConsole.Application;

namespace InfoSafeConsole.Main
{
    public class App
    {
        private readonly IAppService _appService;

        public App(
            IAppService appService)
        {
            _appService = appService;
        }

        public void Run(string[] args)
        {
            _appService.CalculateCustomerAge(1);
        }
    }
}