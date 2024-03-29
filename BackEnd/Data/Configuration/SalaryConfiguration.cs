using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.ToTable("Salary");
            builder.Property(x => x.HoursWorked).HasColumnType("float").IsRequired();
            builder.Property(x => x.HourValue).HasColumnType("float").IsRequired();
            builder.Property(x => x.SalaryTotal).HasColumnType("float").IsRequired();
            builder.Property(x => x.HourType).HasMaxLength(255).IsRequired(false);

            

        }
    }
}
