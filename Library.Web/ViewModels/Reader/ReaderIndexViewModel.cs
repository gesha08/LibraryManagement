namespace Library.Web.ViewModels.Reader
{
    public class ReaderIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int BooksBorrowedCount { get; set; }
    }
}
