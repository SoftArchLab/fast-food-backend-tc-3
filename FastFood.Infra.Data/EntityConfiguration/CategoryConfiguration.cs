using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Linq;

namespace FastFood.Infra.Data.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("tb_category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Name");

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            builder.HasData(
                new Category(1, "Lanche"),
                new Category(2, "Acompanhamento"),
                new Category(3, "Bebida"),
                new Category(4, "Sobremesa")
            );
        }
    }

}
