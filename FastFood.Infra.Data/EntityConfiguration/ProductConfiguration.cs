using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityTypeConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tb_product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.StockQuantity)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.CategoryId)
                .HasColumnName("CategoryId");

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            var seedProduct = Product.Create("X-Burguer", "Pão, carne, queijo, alface e tomate", 15.00m, 100, 1);
            var seedProduct2 = Product.Create("Batata Frita", "Batata frita crocante", 10.00m, 50, 2);

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = seedProduct.Name,
                    Description = seedProduct.Description,
                    Price = seedProduct.Price,
                    StockQuantity = seedProduct.StockQuantity,
                    IsActive = seedProduct.IsActive,
                    CategoryId = seedProduct.CategoryId
                },
                new
                {
                    Id = 2,
                    Name = seedProduct2.Name,
                    Description = seedProduct2.Description,
                    Price = seedProduct2.Price,
                    StockQuantity = seedProduct2.StockQuantity,
                    IsActive = seedProduct2.IsActive,
                    CategoryId = seedProduct2.CategoryId
                }
                );
        }
    }
}
