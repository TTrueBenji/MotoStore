using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MotoStore.Enums;

namespace MotoStore.ViewModels.Positions
{
    public class CreatePositionViewModel
    {
        [DisplayName("Модель")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Минимальная длина: 5. Максимальная: 25")]
        public string Model { get; set; }
        
        [DisplayName("Производитель")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Минимальная длина: 5. Максимальная: 25")]
        public string Manufacturer { get; set; }
        
        [DisplayName("Число тактов")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        public NumberOfCycles NumberOfCycles { get; set; }
        
        [DisplayName("Объем двигателя в кубиках")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [Range(50, 5000, ErrorMessage = "Некорректный объем")]
        public int EngineCapacity { get; set; }
        
        [Attributes.Validation.FileExtensions("jpg, png", ErrorMessage = "Некорректный формат файла")]
        [DisplayName("Загрузить изображение")]
        [Required(ErrorMessage = "Загрузите изображение")]
        public IFormFile Image { get; set; }
        
        [DisplayName("Стоимость")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [Range(10000, 1000000, ErrorMessage = "Некорректное значение")]
        public decimal Price { get; set; }
    }
}