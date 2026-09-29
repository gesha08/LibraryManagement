using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието е задължително")]
        [StringLength(100, ErrorMessage = "Максимум 100 символа")]
        public string Title { get; set; }

        [StringLength(200, ErrorMessage = "Максимум 200 символа")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ISBN е задължителен")]
        [StringLength(13, ErrorMessage = "ISBN трябва да е 13 символа")]
        public string ISBN { get; set; }

        [Range(1, 5000, ErrorMessage = "Страниците трябва да са между 1 и 5000")]
        public int Pages { get; set; }

        public int PublicationYear { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}