using InfoSafeConsole.Application.ViewModels;

namespace InfoSafeConsole.Application.Interfaces
{
    public interface IInfoSafeService
    {
        Task<ContactVM> GetContactByIdAsync(int id);

        Task<List<ContactVM>> GetContactsAsync();

        Task<List<ContactVM>> GetFullContactsAsync();

        Task<HttpResponseMessage> SaveContactAsync(ContactVM value);
    }
}