namespace WafflesBackCommon.Models
{
    public class TurnoModel
    {
        public int? idTurno { get; set; }
        public int? tipoTurno { get; set; }
        public DateTime? fechaTurno { get; set; }
        public TimeSpan? horaDelInicio { get; set; }
        public TimeSpan? horaCierre { get; set; }
        public string? notasInicio { get; set; }
        public string? notasCierre { get; set; }
        public bool? esFeriado { get; set; }
        public int? idEncargadoTurno { get; set; }
        public int? idCaja { get; set; }
        public List<TurnoEmpleadoModel>? Empleados { get; set; }
        public CajaModel? Caja { get; set; }
    }
}