using System.Collections.Generic;
using MotoStore.Models;
using MotoStore.ViewModels.Account;
using MotoStore.ViewModels.Order;
using MotoStore.ViewModels.Positions;
using MotoStore.ViewModels.UsersPersonalArea;

namespace MotoStore.ViewModels.Layout
{
    public class LayoutViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public AllPositionsViewModel AllPositionsViewModel { get; set; }
        public CreatePositionViewModel CreatePositionViewModel { get; set; }
        public OrderCreateViewModel OrderCreateViewModel { get; set; }
        public List<OrderViewModel> OrderViewModels { get; set; }
        public List<User> Users { get; set; }
        public LiveOrderViewModel LiveOrderViewModel { get; set; }
        public ReportViewModel ReportViewModel { get; set; }
        public MessageViewModel MessageViewModel { get; set; }
        public List<UsersPersonalAreaViewModel> ManagerPersonalAreaUsers { get; set; }
    }
}