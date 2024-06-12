using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WafflesBackCommon.Models
{
    public class EmpleadoModel
    {
        public int? idEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoEmpleado { get; set; }
        public string direccionEmpleado { get; set; }
        public string DNIEmpleado { get; set; }
        public string telefonoEmpleado { get; set; }
        public string mailEmpleado { get; set; }
        public int idPuestoEmpleado { get; set; }

    }
}