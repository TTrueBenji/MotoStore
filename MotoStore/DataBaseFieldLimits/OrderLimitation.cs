using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoStore.Models;

namespace MotoStore.DataBaseFieldLimits
{
    public class OrderLimitation : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.PositionId).IsRequired();
        }
    }
}