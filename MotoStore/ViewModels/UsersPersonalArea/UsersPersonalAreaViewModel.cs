using System;

namespace MotoStore.ViewModels.UsersPersonalArea
{
    public class UsersPersonalAreaViewModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public int OrdersCount { get; set; }
    }
}