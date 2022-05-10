using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MotoStore.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [EmailAddress]
        // [RegularExpression (@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Минимальная длина логина: 3. Максимальная: 25")]
        [Remote("CheckUserName", "AccountValidationController", ErrorMessage = "Логин занят")]
        [DisplayName("Логин")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        // [Phone(ErrorMessage = "Некорректный номер телефона")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Некорректный номер телефона")]
        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
        
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        public string Password { get; set; }
        
        [DisplayName("Подтверждение пароля")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
        
        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        public string Address { get; set; }
        
        [DisplayName("Город")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        public string City { get; set; }

        [Attributes.Validation.FileExtensions("jpg, png", ErrorMessage = "Некорректный формат файла")]
        [DisplayName("Загрузить аватар")]
        public IFormFile File { get; set; }
    }
}