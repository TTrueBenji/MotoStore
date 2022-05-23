using MotoStore.Models;

namespace MotoStore.ViewModels.Order
{
    public class LiveOrderViewModel
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public virtual Models.Order Order { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DeliveryAddressViewModel DeliveryAddress { get; set; }
        public string PositionId { get; set; }
        public decimal Price { get; set; }
    }
}