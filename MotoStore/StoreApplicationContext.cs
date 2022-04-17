using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoStore.DatabaseFieldLimits;
using MotoStore.Models;

namespace MotoStore
{
    public class StoreApplicationContext : IdentityDbContext<User>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Order> Orders { get; set; }

        public StoreApplicationContext(DbContextOptions<StoreApplicationContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PositionLimitation());
            builder.ApplyConfiguration(new OrderLimitation());
        }
    }
}