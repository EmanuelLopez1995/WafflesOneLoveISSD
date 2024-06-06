using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class PaymentBoxInitialActiveConfiguration : IEntityTypeConfiguration<PaymentBoxInitialActive>
    {
        public void Configure(EntityTypeBuilder<PaymentBoxInitialActive> builder)
        {
            builder.ToTable("PaymentBoxInitialActive");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.InitialActive).HasColumnType("float").IsRequired(false);

        }
    }
}
