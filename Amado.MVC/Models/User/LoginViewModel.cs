

using Amado.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace Amado.MVC.Models.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email adresi zorunludur!")]
        [MaxLength(50, ErrorMessage = "Email 50 karakterden fazla olamaz!")]
        [EmailAddress(ErrorMessage = "Email adresi formatına uygun olmalıdır!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur!")]
        [Password(ErrorMessage = "Şifreniz en az bir büyük harf, bir küçük harf ve bir sayı içermeli, aynı zamanda en az 8 karakter olmalıdır!")]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
