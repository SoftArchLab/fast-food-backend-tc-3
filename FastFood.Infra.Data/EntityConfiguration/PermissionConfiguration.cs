//using FastFood.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace FastFood.Infra.Data.EntityConfiguration;

//public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
//{
//    public void Configure(EntityTypeBuilder<Permission> builder)
//    {
//        builder.ToTable("tb_permission");

//        builder.HasKey(p => p.Id);

//        builder.Property(p => p.Id)
//            .ValueGeneratedOnAdd()
//            .HasColumnName("Id");

//        builder.Property(p => p.Name)
//            .IsRequired()
//            .HasMaxLength(50)
//            .HasColumnName("Name");

//        builder.HasMany(p => p.Users)
//            .WithOne(u => u.Permission)
//            .HasForeignKey(u => u.PermissionId)
//            .OnDelete(DeleteBehavior.Restrict);
//    }
//}