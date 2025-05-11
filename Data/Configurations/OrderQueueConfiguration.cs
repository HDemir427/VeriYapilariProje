using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Data.Configurations
{
    public class OrderQueueConfiguration : IEntityTypeConfiguration<OrderQueue>
    {
        public void Configure(EntityTypeBuilder<OrderQueue> builder)
        {
            builder.ToTable("OrderQueue");
            
            builder.HasKey(x => x.OrderId);

            builder.Property(x =>x.QueueDate)
                  .IsRequired()
                  .HasDefaultValueSql("GETUTCDATE()") 
                  .ValueGeneratedOnAdd();

            builder.Property(x=> x.ProcessingStatus)
                   .IsRequired()
                   .HasConversion<string>() 
                   .HasMaxLength(20);

            builder.HasOne(x => x.Order)
                   .WithOne(o => o.OrderQueue)
                   .HasForeignKey<OrderQueue>(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
