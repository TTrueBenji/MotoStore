using System.Collections.Generic;
using System.Linq;
using MotoStore.Models;
using MotoStore.ViewModels.Order;

namespace MotoStore.MapConfigurations
{
    public static class OrderMapConfiguration
    {
        public static List<OrderViewModel> MapToOrderViewModels(this IEnumerable<Order> orders)
        {
            return orders.Select(
                order => new OrderViewModel
                {
                    OrderId = order.Id,
                    OrderDate = order.CreationDateTime,
                    PositionInfoViewModel = order.Position.MapToPositionInfoViewModel(),
                    IsCheckouted = order.IsCheckouted,
                    Confirmed = order.Confirmed,
                    Price = order.Price
                }).ToList();
        }

        public static LiveOrderViewModel MapToOrderCheckoutViewModel(this Order order)
        {
            return new LiveOrderViewModel
            {
                Order = order,
                DeliveryAddress = new DeliveryAddressViewModel
                {
                    Address = order.User.Address,
                    ContactNumber = order.User.PhoneNumber
                },
                User = order.User,
                PositionId = order.PositionId
            };
        }
    }
}