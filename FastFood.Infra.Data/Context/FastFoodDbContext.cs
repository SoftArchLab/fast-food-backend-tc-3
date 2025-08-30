using FastFood.Domain.Entities;
using FastFood.Infra.Data.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Context;

public class FastFoodDbContext : DbContext
{
    public FastFoodDbContext(DbContextOptions<FastFoodDbContext> options) : base(options)
    {
        
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PaymentStatus> PaymentStatus { get; set; }
    //public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}