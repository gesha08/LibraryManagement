using System.Collections.Generic;

namespace Library.Web.ViewModels.Reader
{
    public class ReaderDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<BookSimpleViewModel> BorrowedBooks { get; set; }
        public List<BookSimpleViewModel> ReturnedBooks { get; set; }
    }

    public class BookSimpleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
    }
}
