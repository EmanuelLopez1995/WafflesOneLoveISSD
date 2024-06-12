using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafflesBackCommon.Models
{
    public class TurnoEmpleadoModel
    {
        public int? idEmpleado { get; set; }
        public int? idTurno { get; set; }
        public TimeSpan horaIngresoEmpleado { get; set; }
        public string? descripcionIngreso { get; set; }
        public TimeSpan? horaEgresoEmpleado { get; set; }
        public string? descripcionEgreso { get; set; }
        public bool? esRespDeApertCaja { get; set; }
        public bool? esRespDeCierreCaja { get; set; }
        public decimal? sueldoTotalDelDia { get; set; }
    }
}
