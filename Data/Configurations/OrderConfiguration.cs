using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x =>  x.OrderId);

            builder.Property(x => x.UserId).HasMaxLength(100).IsRequired();

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Status).HasMaxLength(50).IsRequired();

            builder.Property(x => x.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        
        }

    }
}
