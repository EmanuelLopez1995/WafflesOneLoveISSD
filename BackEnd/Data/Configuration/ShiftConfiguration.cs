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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.Property(x => x.TypeShift).IsRequired();
            builder.Property(x => x.TypeShiftHoliday).IsRequired(false);
            builder.Property(x => x.Notes).IsRequired(false);

            builder.HasOne(x => x.ClosedByEmployee)
                   .WithMany(x => x.ShiftsClosed)
                   .HasForeignKey(x => x.ClosedByEmployeeId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OpenByEmployee)
                   .WithMany(x => x.ShiftsOpen)
                   .HasForeignKey(x => x.OpenByEmployeeId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
