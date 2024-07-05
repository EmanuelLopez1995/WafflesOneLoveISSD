using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafflesBackCommon.Models
{
    public class CompraModel
    {
        public int? IdCompra { get; set; }
        public DateTime? FechaCompra { get; set; }
        public byte[]? Archivo { get; set; }  // Para almacenar archivos
        public int? IdProveedor { get; set; }
        public decimal? Total { get; set; }
        public List<DetalleCompraModel>? DetallesCompra { get; set; }
    }
}
