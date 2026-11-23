using System.Collections.Generic;

namespace Library.Data.Entities
{
    public class Reader
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<BookReader> BookReaders { get; set; }
    }
}