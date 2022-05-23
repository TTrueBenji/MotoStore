using System;
using System.Linq;
using System.Threading.Tasks;
using CommonData.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Layout;
using MotoStore.ViewModels.Order;

namespace MotoStore.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;

        public OrderController(
            ILogger<AccountController> logger, 
            UserManager<User> userManager, 
            IOrderService orderService, 
            IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _orderService = orderService;
            _emailService = emailService;
        }
        
        [HttpPost]
        public IActionResult Create(LayoutViewModel model)
        {
            try
            {
                Order order = model.OrderCreateViewModel.MapToOrder();
                order.UserId = _userManager.GetUserId(User);
                _logger.LogInformation("{@Controller}.Create данные: {@Data}, USerId: {@UserId}", 
                    GetType(), model.OrderCreateViewModel, order.UserId);
                _orderService.CreateOrder(order);
            
                return RedirectToAction("Info", new {StatusCodes = StatusCodes.Succsess,
                    Message = "Заказ принят, подробную информацию можно получить по ссылке:"});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Info", new {StatusCodes = StatusCodes.Error, 
                    Message = "Ошибка, обратитесь к администратору ссылке:"});
            }
        }

        [HttpGet]
        public IActionResult Checkout(string orderId)
        {
            Order order = _orderService.GetOrderById(orderId);
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                LiveOrderViewModel = order.MapToOrderCheckoutViewModel()
            };
            return View(layoutViewModel);
        }

        [HttpPost]
        public IActionResult Checkout(LayoutViewModel layoutViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("{Controller} Данные: {@Data}",
                        typeof(OrderController), layoutViewModel);
                    _orderService.CreateLiveOrder(layoutViewModel.LiveOrderViewModel);
                    return RedirectToAction(
                        "Index",
                        "UserPersonalArea",
                        new {userId = layoutViewModel.LiveOrderViewModel.UserId});
                }

                _logger.LogWarning("{Controller} модель не прошла валидацию. Данные: {@Data}",
                    typeof(OrderController), layoutViewModel);
                return View(layoutViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError("{Controller} ошибка {@Message}. Данные: {@Data}",
                    typeof(OrderController), e.Message, layoutViewModel);
                return View(layoutViewModel);
            }
        }

        [HttpGet]
        public IActionResult ConfirmDelivery(string orderId)
        {
            _orderService.ConfirmLiveOrder(orderId);
            return RedirectToAction("Index", "UserPersonalArea", new {userId = _userManager.GetUserId(User)});
        }

        [HttpGet]
        public IActionResult Info(StatusCodes statusCodes, string message)
        {
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                MessageViewModel = new MessageViewModel
                {
                    Message = message,
                    Status = statusCodes
                }
            };

            return View(layoutViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetHelp()
        {
            var user = await _userManager.GetUserAsync(User);
            var layout = new LayoutViewModel
            {
                ReportViewModel = new ReportViewModel
                {
                    User = new UserViewModel
                    {
                        Id = user.Id,
                        UserEmail = user.Email,
                        UserName = user.UserName,
                        UserPhone = user.PhoneNumber
                    }
                }
            };
            return View(layout);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(LayoutViewModel viewModel)
        {
            try
            {
                var manager = await _userManager.GetUsersInRoleAsync("Manager");
                await _emailService.SendEmailAsync(
                    manager.First().Email, 
                    $"Требуется помощь {viewModel.ReportViewModel.User.UserName}", 
                    $"Сообщение: {viewModel.ReportViewModel.Message}\n" +
                    $"Адрес для ответа: {viewModel.ReportViewModel.User.UserEmail}\n" +
                    $"Телефон: {viewModel.ReportViewModel.User.UserPhone}\n");

                return RedirectToAction("Info", new {StatusCodes = StatusCodes.Succsess, 
                    Message = "Письмо отправлено"});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Info", new {StatusCodes = StatusCodes.Error, 
                    Message = "Письмо не было отправлено."});
            }
        }
    }
}