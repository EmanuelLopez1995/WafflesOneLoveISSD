namespace Common.Entities
{
    public class Shift
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TypeShiftHoliday { get; set; }
        public String Notes { get; set; }
        public int TypeShift { get; set; }

        public virtual Employee OpenByEmployee { get; set; }
        public int OpenByEmployeeId { get; set; }

        public virtual Employee ClosedByEmployee { get; set; }
        public int? ClosedByEmployeeId { get; set; }

        public virtual ICollection<EmployeeShift> EmployeeShifts { get; set; } = new HashSet<EmployeeShift>();
    }
}