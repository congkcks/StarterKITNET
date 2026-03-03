using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarterKITNET.Entities;

namespace StarterKITNET.Domain
{
    public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransactions");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Type).HasConversion<short>().IsRequired();
            builder.Property(x=>x.Note).HasMaxLength(500);
            builder.Property(x=>x.Quantity).IsRequired(); 
            builder.HasIndex(x => x.Time);
            builder.HasIndex(x => new {x.ProductID,x.Time});

        }
    }
}
