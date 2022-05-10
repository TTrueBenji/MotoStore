using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotoStore.ViewModels.Order
{
    public class DeliveryAddressViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Адрес доставки")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Телефон для связи")]
        public string ContactNumber { get; set; }
        public string Comment { get; set; }
    }
}