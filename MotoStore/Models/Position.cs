using System;
using MotoStore.Enums;

namespace MotoStore.Models
{
    public class Position
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public NumberOfCycles NumberOfCycles { get; set; }
        public int EngineCapacity { get; set; }
        public string PathToImage { get; set; }
    }
}