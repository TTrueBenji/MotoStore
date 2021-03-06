using System;
using MotoStore.ViewModels.Positions;

namespace MotoStore.ViewModels.Order
{
    public class OrderViewModel
    {
        public string OrderId { get; set; }
        public bool IsCheckouted { get; set; }
        public bool Confirmed { get; set; }
        public PositionInfoViewModel PositionInfoViewModel { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
    }
}