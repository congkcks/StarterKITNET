using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarterKITNET.Entities;

namespace StarterKITNET.Domain
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p=>p.Code).IsRequired().HasMaxLength(32).IsRequired();
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(255).IsRequired();
            builder.Property(p=>p.Stock).IsRequired();  
            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}
