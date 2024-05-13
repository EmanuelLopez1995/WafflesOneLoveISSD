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



            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.Shifts)
                   .HasForeignKey(x => x.EmployeeId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
