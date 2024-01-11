using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shifts");
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);

            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.Shifts)
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
