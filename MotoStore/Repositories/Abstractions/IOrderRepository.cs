using System.Collections.Generic;
using MotoStore.Models;
using MotoStore.ViewModels.Order;

namespace MotoStore.Repositories.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        void CancelOrderById(string id);
        void CreateLiveOrder(LiveOrder liveOrder);
        void UpdateLiveOrder(LiveOrder liveOrder);
        LiveOrder GetLiveOrderById(string orderId);
        IEnumerable<Order> GetOrdersByUserId(string id);
    }
}