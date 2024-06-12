using System;

namespace WafflesBackCommon.Models
{
    public class SueldosBasicosModel
    {
        public int? idSueldosBasicos { get; set; }
        public string? descripcion { get; set; }
        public int? valorHoraNormal { get; set; }
        public int? valorHoraFerDom { get; set; }
        public decimal? basicoDueno { get; set; }
        public decimal? plusEncargado { get; set; }
    }
}