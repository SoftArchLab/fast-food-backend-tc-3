using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Infra.Data.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("tb_user");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("Name");

        builder.Property(u => u.TaxId)
            .HasColumnName("TaxId");

        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .HasColumnName("Email");

        builder.Property(u => u.Password)
            .HasMaxLength(150)
            .HasColumnName("Password");

        builder.Property(u => u.Role)
            .HasColumnName("UserRole");

        var seedAdmin = User.Create("admin", "99999999999", "admin@email.com", "admin", Domain.Enums.UserRole.Admin);
        var seedCustomer = User.Create("customer", "88888888888", "customer@email.com", "customer", Domain.Enums.UserRole.Customer);

        builder.HasData(
            new
            {
                Id = Guid.NewGuid(),
                seedAdmin.Name,
                seedAdmin.TaxId,
                seedAdmin.Email,
                seedAdmin.Password,
                seedAdmin.Role,
            },
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                seedCustomer.Name,
                seedCustomer.TaxId,
                seedCustomer.Email,
                seedCustomer.Password,
                seedCustomer.Role,
            }
        );


    }
}