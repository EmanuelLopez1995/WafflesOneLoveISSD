namespace Common.Model
{
    public class EmployeeShiftModel
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}