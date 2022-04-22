using System;
using Microsoft.AspNetCore.Identity;

namespace MotoStore.Models
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}