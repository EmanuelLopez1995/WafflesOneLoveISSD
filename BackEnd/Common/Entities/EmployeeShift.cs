namespace Common.Entities
{
    public class EmployeeShift
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}