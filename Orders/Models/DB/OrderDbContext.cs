using Microsoft.EntityFrameworkCore;

namespace Orders.Models.DB
{
    public class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                        .Property(p => p.Quantity)
                        .HasPrecision(9, 4);

            modelBuilder.Entity<Order>()
                        .Property(p => p.Date)
                        .HasColumnType("datetime2");

            modelBuilder.Entity<Provider>().HasData(
            new Provider[]
            {
                new Provider ( 1, "Первый поставщик"),
                new Provider ( 2, "Второй поставщик"),
                new Provider ( 3, "Третий поставщик")
            });
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Provider> Provider { get; set; }
    }
}
