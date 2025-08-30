using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.ToTable("tb_payment_status");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(ps => ps.StatusName)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasColumnName("PaymentStatus");

            // Seed data if necessary
            builder.HasData(
                new 
                {
                    Id = 1,
                    StatusName = PaymentStatusEnum.Pending
                },
                new 
                {
                    Id = 2,
                    StatusName = PaymentStatusEnum.InProcess
                },
                new 
                {
                    Id = 3,
                    StatusName = PaymentStatusEnum.Approved
                },
                new 
                {
                    Id = 4,
                    StatusName = PaymentStatusEnum.Cancelled
                },
                new 
                {
                    Id = 5,
                    StatusName = PaymentStatusEnum.Rejected
                }
            );
        }
    }
}
