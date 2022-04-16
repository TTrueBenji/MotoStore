using System.Collections.Generic;

namespace MotoStore.ViewModels.Positions
{
    public class AllPositionsViewModel
    {
        public List<PositionShortInfoViewModel> Positions { get; set; }
        public PositionInfoViewModel PositionInfo { get; set; }
    }
}