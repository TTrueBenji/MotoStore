using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoStore.Models;

namespace MotoStore
{
    public class StoreApplicationContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public StoreApplicationContext(DbContextOptions<StoreApplicationContext> options) : base(options){ }
    }
}