using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("tb_order_status");

            builder.HasKey(os => os.Id);

            builder.Property(os => os.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(os => os.Name)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .HasColumnName("OrderStatus");

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = Domain.Enums.OrderStatusEnum.Received
                }
                ,new
                {
                    Id = 2,
                    Name = Domain.Enums.OrderStatusEnum.InPreparation
                }
                ,new
                {
                    Id = 3,
                    Name = Domain.Enums.OrderStatusEnum.Ready
                }
                ,new
                {
                    Id = 4,
                    Name = Domain.Enums.OrderStatusEnum.Finished
                }
            );
        }
    }
}
