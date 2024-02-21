using InfoSafeConsole.Application.Extensions;
using InfoSafeConsole.Application.Interfaces;
using InfoSafeConsole.Application.ViewModels;
using Microsoft.Extensions.Logging;

namespace InfoSafeConsole.Application
{
    public class InfoSafeService : IInfoSafeService
    {
        private readonly ILogger<InfoSafeService> _logger;
        private readonly HttpClient _client;
        public readonly string _baseUrl;

        public InfoSafeService(
            ILogger<InfoSafeService> logger,
            HttpClient client)
        {
            _logger = logger;
            _client = client;
            _baseUrl = "api/Contact";
        }

        #region Contact

        public async Task<List<ContactVM>> GetContactsAsync()
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(InfoSafeService), nameof(GetContactsAsync));

            var url = $"{_baseUrl}/GetContacts";
            var response = await _client.GetAsync(url);
            return await response.ReadContentAsAsync<List<ContactVM>>();
        }

        public async Task<ContactVM> GetContactByIdAsync(int id)
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(InfoSafeService), nameof(GetContactByIdAsync));

            var url = $"{_baseUrl}/GetContactById/{id}";
            var response = await _client.GetAsync(url);
            return await response.ReadContentAsAsync<ContactVM>();
        }

        public async Task<List<ContactVM>> GetFullContactsAsync()
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(InfoSafeService), nameof(GetFullContactsAsync));

            var url = $"{_baseUrl}/GetFullContacts";
            var response = await _client.GetAsync(url);
            return await response.ReadContentAsAsync<List<ContactVM>>();
        }

        public async Task<HttpResponseMessage> SaveContactAsync(ContactVM value)
        {
            _logger.LogInformation("{Class}.{Action} start", nameof(InfoSafeService), nameof(SaveContactAsync));

            var url = $"{_baseUrl}/SaveContact";
            return await _client.PostAsJsonAsync(url, value);
        }

        #endregion Contact
    }
}