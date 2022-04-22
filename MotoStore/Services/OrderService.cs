using System.Collections.Generic;
using MotoStore.Exceptions;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetPositions() => _orderRepository.GetAll();
        public void CreateOrder(Order order) => _orderRepository.Create(order);

        public void UpdateOrder(Order order) => _orderRepository.Update(order);

        public void DeleteOrderById(string id) => _orderRepository.DeleteById(id);

        public void CancelOrder(string id) => _orderRepository.CancelOrderById(id);

        public IEnumerable<Order> GetOrdersByUserId(string id) => _orderRepository.GetOrdersByUserId(id);
        
        public Order GetPositionById(string id)
        {
            var order = _orderRepository.GetById(id);
            if (order is null)
                throw new EntityNotFoundException(nameof(Position), id);

            return order;
        }
    }
}