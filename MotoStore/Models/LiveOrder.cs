namespace MotoStore.Models
{
    public class LiveOrder
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string Addres { get; set; }
        public string ContactNumber { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string PositionId { get; set; }
        public virtual Position Position { get; set; }
        public bool Confirmed { get; set; }
        public decimal Price { get; set; }
    }
}