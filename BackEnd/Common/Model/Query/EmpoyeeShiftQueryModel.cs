namespace Common.Model.query
{
    public class EmpoyeeShiftQueryModel
    {
        public int? EmployeeId { get; set; }
        public bool? SinFinalizar { get; set; }
        public bool? SinFinalizarHora { get; set; }
        public int shiftId { get; set; }
    }
}