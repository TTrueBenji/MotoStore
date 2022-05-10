using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MotoStore.Exceptions;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Order;

namespace MotoStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public OrderService(
            IOrderRepository orderRepository, 
            IEmailService emailService, 
            UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetPositions() => _orderRepository.GetAll();
        public void CreateOrder(Order order) => _orderRepository.Create(order);
        public async Task CreateLiveOrder(LiveOrderViewModel liveOrderViewModel)
        {
            LiveOrder liveOrder = liveOrderViewModel.MapToOrderCheckoutViewModel();
            var order = _orderRepository.GetById(liveOrder.OrderId);
            order.IsCheckouted = true;
            _orderRepository.Update(order);
            _orderRepository.CreateLiveOrder(liveOrder);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == liveOrder.UserId);
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            var manager = managers.ToList().FirstOrDefault();
            Task sendMailToUser = _emailService.SendEmailAsync(user?.Email, $"{user?.UserName}, Заказ оформлен",
                "Подробнее по телефону +77777777777");
            Task sendMailToManager =
                    _emailService.SendEmailAsync(manager?.Email, $"{manager?.UserName}, поступил заказ", 
                $"Имя клиента: {user?.UserName}\n" +
                $"Телефон для связи: {user?.PhoneNumber}\n" +
                $"Адрес доставки: {user?.Address}\n" +
                $@"<a href='https://localhost:5001/ManagerPersonalArea?userId={manager?.Id}'>Перейти в личный кабинет</a>");

            await sendMailToManager;
            await sendMailToUser;
        }

        public void UpdateOrder(Order order) => _orderRepository.Update(order);

        public void DeleteOrderById(string id) => _orderRepository.DeleteById(id);

        public void CancelOrder(string id) => _orderRepository.CancelOrderById(id);

        public IEnumerable<Order> GetOrdersByUserId(string id) => _orderRepository.GetOrdersByUserId(id).ToList();
        public Order GetOrderById(string id)
        {
            return _orderRepository.GetById(id);
        }

        public void ConfirmLiveOrder(string orderId)
        {
            LiveOrder liveOrder = _orderRepository.GetLiveOrderById(orderId);
            Order order = _orderRepository.GetById(orderId);
            liveOrder.Confirmed = true;
            order.Confirmed = true;
            _orderRepository.UpdateLiveOrder(liveOrder);
            _orderRepository.Update(order);
        }


        public Order GetPositionById(string id)
        {
            var order = _orderRepository.GetById(id);
            if (order is null)
                throw new EntityNotFoundException(nameof(Position), id);

            return order;
        }
    }
}