using Common.Model.Enum;

namespace Common.Model
{
    public class ShiftModel
    {
        public int OpenEmployeeId { get; set; }
        public int? ClosedEmployeeId { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TypeShiftEnum TypeShift { get; set; }
        public TypeShiftHolidayEnum? TypeShiftHoliday { get; set; }
        public string? Notes { get; set; }

        public IEnumerable<EmployeeShiftModel>? EmployeeShifts { get; set; }

    }
}