namespace Library.Web.ViewModels.Book
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public int PublicationYear { get; set; }
        public string AuthorName { get; set; }
        public DateTime AuthorBirthDate { get; set; }
    }
}