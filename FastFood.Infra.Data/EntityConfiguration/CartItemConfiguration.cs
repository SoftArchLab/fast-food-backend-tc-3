using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("tb_cart_item");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(ci => ci.Id)
            .IsRequired()
            .HasColumnName("Id");
        
        builder.Property(ci => ci.Quantity)
            .IsRequired()
            .HasColumnName("Quantity");

        builder.HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        var seedCartItem1 = CartItem.Create(1, 1, 1);
        var seedCartItem2 = CartItem.Create(1, 2, 1);
        builder.HasData(
            new
            {
                Id = 1,
                Quantity = seedCartItem1.Quantity,
                ProductId = seedCartItem1.ProductId,
                CartId = seedCartItem1.CartId
            },
            new
            {
                Id = 2,
                Quantity = seedCartItem2.Quantity,
                ProductId = seedCartItem2.ProductId,
                CartId = seedCartItem2.CartId
            }
        );
    }
}