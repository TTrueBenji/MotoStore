using System;

namespace MotoStore.Models
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string PositionId { get; set; }
        public virtual Position Position { get; set; }
        public bool Deleted { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsCheckouted { get; set; }
        public bool Confirmed { get; set; }
    }
}