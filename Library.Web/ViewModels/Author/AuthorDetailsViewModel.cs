namespace Library.Web.ViewModels.Author
{
    public class AuthorDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate { get; set; }
        public List<BookSimpleViewModel> Books { get; set; }
    }

    public class BookSimpleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
    }
}
