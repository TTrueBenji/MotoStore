using MotoStore.Models;
using MotoStore.ViewModels.Order;

namespace MotoStore.MapConfigurations
{
    public static class LiveOrderMapConfiguration
    {
        public static LiveOrder MapToOrderCheckoutViewModel(this LiveOrderViewModel viewModel)
        {
            return new LiveOrder
            {
                Addres = viewModel.DeliveryAddress.Address,
                Comment = viewModel.DeliveryAddress.Comment,
                ContactNumber = viewModel.DeliveryAddress.ContactNumber,
                OrderId = viewModel.OrderId,
                PositionId = viewModel.PositionId,
                UserId = viewModel.UserId
            };
        }
    }
}