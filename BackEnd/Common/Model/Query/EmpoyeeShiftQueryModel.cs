namespace Common.Model.query
{
    public class EmpoyeeShiftQueryModel
    {
        public int? ShiftId { get; set; }
        public int? EmployeeId { get; set; }
        public bool? SinFinalizar { get; set; }
        public bool? SinFinalizarHora { get; set; }

    }
}