using System;
using System.ComponentModel.DataAnnotations.Schema;
using MotoStore.Enums;

namespace MotoStore.Models
{
    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public NumberOfCycles NumberOfCycles { get; set; }
        public int EngineCapacity { get; set; }
        public string PathToImage { get; set; }

        public bool Deleted { get; set; }
        //TODO: Проработать
        // public decimal Price { get; set; }
    }
}