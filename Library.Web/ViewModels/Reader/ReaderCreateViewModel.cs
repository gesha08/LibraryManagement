using System.ComponentModel.DataAnnotations;

namespace Library.Web.ViewModels.Reader
{
    public class ReaderCreateViewModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Максимум 100 символа")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Максимум 20 символа")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Датата на регистрация е задължителна")]
        public DateTime RegistrationDate { get; set; }
    }
}