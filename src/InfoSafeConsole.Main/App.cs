using InfoSafeConsole.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
