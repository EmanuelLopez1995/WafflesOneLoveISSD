using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Dni).HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired(false);
            builder.Property(x => x.Direction).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Position).HasMaxLength(100).IsRequired(false);
        }
    }
}
