using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Минимальная длина: 5. Максимальная: 25")]
        public string Manufacturer { get; set; }
        
        [DisplayName("Число тактов")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        public NumberOfCycles NumberOfCycles { get; set; }
        
        [DisplayName("Объем двигателя в кубиках")]
        [Required(ErrorMessage = "Поле обязательно для ввода.")]
        [Range(50, 5000, ErrorMessage = "Некорректный объем")]
        public int EngineCapacity { get; set; }
        
        [NotMapped]
        [Attributes.Validation.FileExtensions("jpg,png", ErrorMessage = "Некорректный формат файла")]
        [DisplayName("Загрузить изображение")]
        public IFormFile Image { get; set; }
    }
}