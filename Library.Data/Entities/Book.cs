using System.Collections.Generic;

namespace Library.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBN { get; set; }

        public int Pages { get; set; }

        public int PublicationYear { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public List<BookReader> BookReaders { get; set; }
    }
}