using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class PaymentBoxConfiguration : IEntityTypeConfiguration<PaymentBox>
    {
        public void Configure(EntityTypeBuilder<PaymentBox> builder)
        {
            builder.ToTable("PaymentBox");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CashWitdrawal).HasColumnType("float").IsRequired(false); 
            builder.Property(x => x.InitialActive).HasColumnType("float").IsRequired(false);
            builder.Property(x => x.InitialImport).HasColumnType("float").IsRequired(false);
            builder.Property(x => x.FinalImport).HasColumnType("float").IsRequired(false);
           
            
          

        }
    }
}
