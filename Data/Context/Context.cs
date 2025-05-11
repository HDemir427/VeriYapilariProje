using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Configurations;
using OrderManagementSystem.Data.Entity;

namespace OrderManagementSystem.Data.Context
{
    public class Context : DbContext
    {
        public DbSet<Category>  Categories{ get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<OrderQueue> OrderQueues{ get; set; }

        public DbSet<Product> Products{ get; set; }

        public DbSet<User> Users{ get; set; }

        public DbSet<UserHistory> UserHistories{ get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbOrderManagementSystem;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderQueueConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserHistoryConfiguration());
          
        }

    }
}
