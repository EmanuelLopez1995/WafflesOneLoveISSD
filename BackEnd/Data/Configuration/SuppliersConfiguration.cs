using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class SuppliersConfiguration : IEntityTypeConfiguration<Suppliers>
    {
        public void Configure(EntityTypeBuilder<Suppliers> builder)
        {
            builder.ToTable("Suppliers");
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.SocialReason).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Addrees).HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.Cuit).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Detail).HasMaxLength(255).IsRequired(false);
        }
    }
}
