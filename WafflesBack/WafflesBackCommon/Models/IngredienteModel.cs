namespace WafflesBackCommon.Models
{
    public class IngredienteModel
    {
        public int? IdIngrediente { get; set; }
        public string? nombreIngrediente { get; set; }
        public decimal stockMinimo { get; set; }
        public decimal stockActual { get; set; }
        public string? detalleIngrediente { get; set; }
        public int idUMD { get; set; }

        public List<ArticuloPorIngredienteModel>? Articulos { get; set; }
    }
}