namespace Library.Web.ViewModels.Author
{
    public class AuthorIndexViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int BooksCount { get; set; }
    }
}