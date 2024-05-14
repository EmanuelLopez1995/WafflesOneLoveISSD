using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stock");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.ProductBrand).IsRequired();
            builder.Property(x => x.ActualStock).IsRequired(false);
            builder.Property(x => x.MinimunStock).IsRequired(false);
            builder.Property(x => x.MinimunStock).IsRequired(false);
            builder.Property(x => x.Detail).IsRequired(false);




        }
    }
}
