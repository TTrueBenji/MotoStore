using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoStore.Models;

namespace MotoStore
{
    public class StoreApplicationContext : IdentityDbContext<User>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }

        public StoreApplicationContext(DbContextOptions<StoreApplicationContext> options) : base(options){ }
    }
}