using System.ComponentModel.DataAnnotations;

namespace Library.Web.ViewModels.Book
{
    public class BookEditViewModel
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

        [Required(ErrorMessage = "Годината на публикация е задължителна")]
        [Range(1000, 2030, ErrorMessage = "Невалидна година")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Моля, изберете автор")]
        public int AuthorId { get; set; }
    }
}
