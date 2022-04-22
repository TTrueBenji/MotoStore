using System.Collections.Generic;
using System.Linq;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;

namespace MotoStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreApplicationContext _db;

        public OrderRepository(StoreApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> GetAll() => _db.Orders;
        public Order GetById(string id) => _db.Orders.FirstOrDefault(o => o.Id == id);
        
        public void Create(Order order){
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public void Update(Order order)
        {
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void DeleteById(string id)
        {
            var order = _db.Orders.FirstOrDefault(p => p.Id == id);
            if (order == null) return;
            order.Deleted = true;
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void CancelOrderById(string id)
        {
            var order = _db.Orders.FirstOrDefault(p => p.Id == id);
            if (order == null) return;
            order.IsCanceled = true;
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByUserId(string id) => 
            _db.Orders.Where(o => o.UserId == id);
    }
}