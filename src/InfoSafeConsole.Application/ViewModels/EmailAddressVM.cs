namespace InfoSafeConsole.Application.ViewModels
{
    public class EmailAddressVM
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; } = null!;
    }
}