using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("tb_cart");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(c => c.UserId)
            .HasColumnName("UserId")
            .IsRequired();

        builder.Property(c => c.Subtotal)
            .IsRequired()
            .HasColumnName("Subtotal");
        
        builder.Property(c => c.IsFinished)
            .IsRequired()
            .HasColumnName("IsFinished");

        builder.HasMany(c => c.CartItems)
            .WithOne(c => c.Cart)
            .HasForeignKey(c => c.CartId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        var seedCart = Cart.CreateCart(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        builder.HasData(
            new
            {
                Id = 1,
                UserId = seedCart.UserId,
                Subtotal = 25.00m,
                IsFinished = false
            }
        );
    }
}