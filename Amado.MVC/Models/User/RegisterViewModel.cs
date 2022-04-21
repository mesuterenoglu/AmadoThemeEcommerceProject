

using Amado.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace Amado.MVC.Models.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "İsim zorunludur!")]
        [MaxLength(100, ErrorMessage = "İsim 100 karakterden fazla olamaz!")]
        [Display(Name ="İsim")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur!")]
        [MaxLength(200, ErrorMessage = "Soyisim 200 karakterden fazla olamaz!")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email adresi zorunludur!")]
        [MaxLength(50, ErrorMessage = "Email 50 karakterden fazla olamaz!")]
        [EmailAddress(ErrorMessage ="Email adresi formatına uygun olmalıdır!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur!")]
        [Password(ErrorMessage = "Şifreniz en az bir büyük harf, bir küçük harf ve bir sayı içermeli, aynı zamanda en az 8 karakter olmalıdır!")]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre doğrulaması zorunludur!")]
        [Compare("Password", ErrorMessage = "Şifre ve doğrulama birbiri ile eşleşmelidir!")]
        [Display(Name = "Parola Tekrar")]
        public string ConfirmPassword { get; set; }
    }
}
