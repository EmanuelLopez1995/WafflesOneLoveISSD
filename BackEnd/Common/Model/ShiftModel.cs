using Common.Model.Enum;

namespace Common.Model
{
    public class ShiftModel
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TypeShiftEnum TypeShift { get; set; }

    }
}