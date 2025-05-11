using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Data.Configurations
{
    public class UserHistoryConfiguration : IEntityTypeConfiguration<UserHistory>
    {
        public void Configure(EntityTypeBuilder<UserHistory> builder)
        {
           
            builder.ToTable("UserHistories");

      
            builder.HasKey(uh => uh.HistoryId);

        
            builder.Property(uh => uh.UserId)
                   .IsRequired();

           
            builder.Property(uh => uh.ActionType)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

           
            builder.Property(uh => uh.ActionDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()")
                   .ValueGeneratedOnAdd();
  
            builder.HasOne(uh => uh.User)
                   .WithMany(u => u.UserHistories)
                   .HasForeignKey(uh => uh.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uh => uh.Product)
                   .WithMany()
                   .HasForeignKey(uh => uh.ProductId)
                   .OnDelete(DeleteBehavior.SetNull);

            

           
        }
    }
}
