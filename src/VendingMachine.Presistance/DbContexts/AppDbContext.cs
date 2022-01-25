using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Prisistence.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(t => t.CurrencyUnit).IsRequired();
                o.Property(t => t.State).HasConversion<string>();
            });

            builder.Entity<Product>(o =>
            {
                o.HasKey(e => e.Id);
                o.Property(t => t.Name).IsRequired().HasMaxLength(255);
            });

            builder.Entity<OrderProduct>(o =>
            {
                o.HasKey(t => t.Id);
                o.HasOne(t => t.Order).WithMany(t => t.OrderProducts).HasForeignKey(t => t.OrderId);
                o.HasOne(t => t.Product).WithMany(t => t.OrderProducts).HasForeignKey(t => t.ProductId);
            });
        }
    }
}
