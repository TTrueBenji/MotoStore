using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotoStore.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}