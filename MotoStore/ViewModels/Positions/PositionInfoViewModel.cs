﻿using MotoStore.Enums;

namespace MotoStore.ViewModels.Positions
{
    public class PositionInfoViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public NumberOfCycles NumberOfCycles { get; set; }
        public int EngineCapacity { get; set; }
        public string PathToImage { get; set; }
    }
}