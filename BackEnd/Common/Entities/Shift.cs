namespace Common.Entities
{
    public class Shift
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TypeShift { get; set; }
        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public int? TypeShiftHoliday { get; set; }
        public String Notes { get; set; }

      

    }
}