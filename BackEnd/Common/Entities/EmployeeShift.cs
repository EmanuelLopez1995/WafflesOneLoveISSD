namespace Common.Entities
{
    public class EmployeeShift
    {

        public int Id { get; set; }
        public DateTime  StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartTimeHours { get; set; }
        public int StartTimeMinutes { get; set; }
        public int? EndTimeHours { get; set; }
        public int? EndTimeMinutes { get; set; }
        public virtual Employee Employee { get; set; }
        public string NotesAdmission { get; set; }
        public bool cashier { get; set; }
        public int EmployeeId { get; set; }
        public string NotesEnd{ get; set; }


    }

}