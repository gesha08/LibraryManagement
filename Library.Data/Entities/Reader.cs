using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    public class Reader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Максимум 100 символа")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Максимум 20 символа")]
        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<BookReader> BookReaders { get; set; }
    }
}