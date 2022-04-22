using System.Collections.Generic;
using MotoStore.Models;

namespace MotoStore.Repositories.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        void CancelOrderById(string id);
        IEnumerable<Order> GetOrdersByUserId(string id);
    }
}