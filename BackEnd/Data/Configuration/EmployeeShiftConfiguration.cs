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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.Property(x => x.StartTimeHours).IsRequired();
            builder.Property(x => x.StartTimeMinutes).IsRequired();
            builder.Property(x => x.EndTimeHours).IsRequired(false);
            builder.Property(x => x.EndTimeMinutes).IsRequired(false);
            builder.Property(x => x.NotesAdmission).IsRequired(false);
            builder.Property(x => x.NotesEnd).IsRequired(false);
            builder.Property(x => x.cashier).IsRequired();


            // Nuevas propiedades para la hora


            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.EmployeeShifts)
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Shift)
                   .WithMany(x => x.EmployeeShifts)
                   .HasForeignKey(x => x.ShiftId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);

          
        }
    }
}
