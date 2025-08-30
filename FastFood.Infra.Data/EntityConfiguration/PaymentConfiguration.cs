using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("tb_payment");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(p => p.OrderId)
                .IsRequired()
                .HasColumnName("OrderId");

            builder.Property(p => p.Method)
                .IsRequired()
                .HasColumnName("Method");

            builder.Property(p => p.PaymentDate)
                .IsRequired()
                .HasColumnName("PaymentDate");
            
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnName("Price");
            
            builder.Property(p => p.PaymentIdMP)
                .IsRequired()
                .HasColumnName("PaymentIdMP");

            builder.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);

            //builder.OwnsOne(p => p.PaymentStatus, ownedNavigationBuilder =>
            //{
            //    ownedNavigationBuilder.ToTable("tb_payment_status");

            //    ownedNavigationBuilder.HasKey(ps => ps.Id);

            //    ownedNavigationBuilder.Property(ps => ps.Id)
            //         .ValueGeneratedOnAdd()
            //         .HasColumnName("Id");

            //    ownedNavigationBuilder.Property(ps => ps.StatusName)
            //        .HasConversion<string>()
            //        .HasMaxLength(50)
            //        .HasColumnName("PaymentStatus");

            //});

            builder.HasOne(p => p.PaymentStatus)
            .WithOne(ps => ps.Payment)
            .HasForeignKey<Payment>(o => o.PaymentStatusId);
        }
    }
}
