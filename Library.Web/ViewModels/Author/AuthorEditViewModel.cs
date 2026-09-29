using System.ComponentModel.DataAnnotations;

namespace Library.Web.ViewModels.Author
{
    public class AuthorEditViewModel
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

        [Required(ErrorMessage = "Датата на раждане е задължителна")]
        public DateTime BirthDate { get; set; }
    }
}