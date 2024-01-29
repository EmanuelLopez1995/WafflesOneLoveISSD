using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FC.Data.Configuration
{
    public class EmployeeShiftConfiguration : IEntityTypeConfiguration<EmployeeShift>
    {
        public void Configure(EntityTypeBuilder<EmployeeShift> builder)
        {
            builder.ToTable("EmployeeShifts");
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.Property(x => x.StartTimeHours).IsRequired();
            builder.Property(x => x.StartTimeMinutes).IsRequired();
            builder.Property(x => x.EndTimeHours).IsRequired(false);
            builder.Property(x => x.EndTimeMinutes).IsRequired(false);
            builder.Property(x => x.Notes).IsRequired(false);
            builder.Property(x => x.cashier).IsRequired();


            // Nuevas propiedades para la hora


            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.EmployeeShifts)
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
