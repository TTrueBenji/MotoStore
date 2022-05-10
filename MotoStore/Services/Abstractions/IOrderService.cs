using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.Models;
using MotoStore.ViewModels.Order;

namespace MotoStore.Services.Abstractions
{
    public interface IOrderService
    {
        IEnumerable<Order> GetPositions();
        Order GetPositionById(string id);
        void CreateOrder(Order item);
        Task CreateLiveOrder(LiveOrderViewModel liveOrder);
        void UpdateOrder(Order item);
        void DeleteOrderById(string id);
        void CancelOrder(string id);
        IEnumerable<Order> GetOrdersByUserId(string id);
        Order GetOrderById(string id);
        void ConfirmLiveOrder(string orderId);
    }
}