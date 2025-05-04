using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x=> x.CategoryId);
            
            builder.Property(x=> x.Name).HasMaxLength(100).IsRequired();
            
            builder.Property(x => x.Description).HasMaxLength(250).IsRequired();
            
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();

        }
    }
}
