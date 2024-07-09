using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WafflesBackCommon.Models
{
    public class ArticuloModel
    {
        public int? IdArticulo { get; set; }
        public string? nombreArticulo { get; set; }
        public string? marcaArticulo { get; set; }
        public decimal stockMinimo { get; set; }
        public decimal stockActual { get; set; }
        public bool esMateriaPrima { get; set; }
        public decimal pesoArticulo { get; set; }
        public string? detalleArticulo { get; set; }
        public int idUMD { get; set; }
        public int IdIngrediente { get; set; }

    }
}