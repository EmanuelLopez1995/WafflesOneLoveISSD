namespace Common.Model
{
    public class EmployeeShiftModel
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartTimeHours { get; set; }
        public int StartTimeMinutes { get; set; }
        public int? EndTimeHours { get; set; }
        public int? EndTimeMinutes { get; set; }
        public string NotesAdmission { get; set; }
        public string NotesEnd { get; set; }
        public bool cashier { get; set; }



    }

}