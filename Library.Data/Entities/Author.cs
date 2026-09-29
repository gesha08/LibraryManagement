using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string LastName { get; set; }

        [StringLength(500, ErrorMessage = "Максимум 500 символа")]
        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Book> Books { get; set; }
    }
}