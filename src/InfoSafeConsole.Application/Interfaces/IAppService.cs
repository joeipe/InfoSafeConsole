namespace InfoSafeConsole.Application.Interfaces
{
    public interface IAppService
    {
        Task<int> CalculateCustomerAgeAsync(int id);
    }
}