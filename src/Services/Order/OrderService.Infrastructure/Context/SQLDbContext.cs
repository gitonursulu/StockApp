using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;

namespace OrderService.Infrastructure.Context
{
    public class SQLDbContext : DbContext
    {
        public SQLDbContext(DbContextOptions<SQLDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order entity için yapılandırma
            modelBuilder.Entity<Order>(entity =>
            {
                // Primary key tanımı
                entity.HasKey(e => e.Id);

                // OrderAddress değer nesnesi için yapılandırma
                entity.OwnsOne(o => o.OrderAddress, address =>
                {
                    address.WithOwner();

                    // Opsiyonel olarak, değer nesnesi için tablo ismi ve sütun isimleri belirleyebilirsiniz
                    address.Property(p => p.City).HasColumnName("City");
                    address.Property(p => p.PostCode).HasColumnName("PostCode");
                });
            });

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
