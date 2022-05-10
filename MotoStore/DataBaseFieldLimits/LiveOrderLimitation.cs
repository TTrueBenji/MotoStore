using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoStore.Models;

namespace MotoStore.DataBaseFieldLimits
{
    public class LiveOrderLimitation : IEntityTypeConfiguration<LiveOrder>
    {
        public void Configure(EntityTypeBuilder<LiveOrder> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o => o.OrderId).IsRequired();
            builder.Property(o => o.PositionId).IsRequired();
        }
    }
}