using System.Collections.Generic;
using MotoStore.Models;

namespace MotoStore.Services.Abstractions
{
    public interface IOrderService
    {
        IEnumerable<Order> GetPositions();
        Order GetPositionById(string id);
        void CreateOrder(Order item);
        void UpdateOrder(Order item);
        void DeleteOrderById(string id);
        void CancelOrder(string id);
        IEnumerable<Order> GetOrdersByUserId(string id);
    }
}