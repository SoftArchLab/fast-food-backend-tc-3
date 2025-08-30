using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("tb_order");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(o => o.UserId)
            .HasColumnName("UserId");

        builder.Property(o => o.Total)
            .IsRequired()
            .HasColumnName("Total");

        builder.Property(o => o.CreatedDate)
            .IsRequired()
            .HasColumnName("CreatedDate");

        builder.Property(o => o.CompletionDate)
            .HasColumnName("CompletionDate");

        builder.Property(o => o.PaymentId)
            .HasColumnName("PayemntId");
        
        builder.Property(o => o.OrderStatusId)
            .HasColumnName("OrderStatusId");

        //builder.OwnsOne(o => o.Status, ownedNavigationBuilder =>
        //{

        //    ownedNavigationBuilder.ToTable("tb_order_status");

        //    ownedNavigationBuilder.HasKey(os => os.Id);

        //    ownedNavigationBuilder.Property(os => os.Id)
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("Id");

        //    ownedNavigationBuilder.Property(os => os.Name)
        //        .HasConversion<string>()
        //        .HasMaxLength(50)
        //        .HasColumnName("OrderStatus");
        //});

        builder.HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Order>(o => o.PaymentId);

        builder.HasOne(o => o.Status)
            .WithOne(os => os.Order)
            .HasForeignKey<Order>(o => o.OrderStatusId);

    }
}