using System.Collections.Generic;
using System.Linq;
using MotoStore.Models;
using MotoStore.ViewModels.Order;
using MotoStore.ViewModels.Positions;

namespace MotoStore.MapConfigurations
{
    public static class OrderMapConfiguration
    {
        public static IEnumerable<OrderViewModel> MapToOrderViewModels(this IEnumerable<Order> orders)
        {
            return orders.Select(
                order => new OrderViewModel
                {
                    OrderDate = order.CreationDateTime,
                    PositionInfoViewModel = PositionToPositionInfoViewModel(order.Position)
                });
        }

        private static PositionInfoViewModel PositionToPositionInfoViewModel(Position position)
        {
            return new PositionInfoViewModel
            {
                Id = position.Id,
                Manufacturer = position.Manufacturer,
                EngineCapacity = position.EngineCapacity,
                Model = position.Model,
                Price = position.Price,
                NumberOfCycles = position.NumberOfCycles,
                PathToImage = position.PathToImage
            };
        }
    }
}