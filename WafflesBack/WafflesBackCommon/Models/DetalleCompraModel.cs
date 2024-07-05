using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafflesBackCommon.Models
{
    public class DetalleCompraModel
    {
        public int? IdDetalleCompra { get; set; }
        public int? IdArticulo { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
        public int? IdCompra { get; set; }
    }
}
