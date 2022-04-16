using MotoStore.ViewModels.Positions;

namespace MotoStore.ViewModels.Account
{
    public class LayoutViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public AllPositionsViewModel AllPositionsViewModel { get; set; }
        public CreatePositionViewModel CreatePositionViewModel { get; set; }
        public PositionInfoViewModel PositionInfoViewModel { get; set; }
    }
}