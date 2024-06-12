using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafflesBackCommon.Models
{
    public class CajaModel
    {
        public int? idCaja { get; set; }
        public decimal activoInicial { get; set; }
        public decimal? retiroCaja { get; set; }
        public decimal importeInicial { get; set; }
        public decimal? importeFinal { get; set; }
    }
}
