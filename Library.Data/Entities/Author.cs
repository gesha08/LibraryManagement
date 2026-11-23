using System.Collections.Generic;

namespace Library.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Book> Books { get; set; }
    }
}