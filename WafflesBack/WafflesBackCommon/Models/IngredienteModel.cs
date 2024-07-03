namespace WafflesBackCommon.Models
{
    public class IngredienteModel
    {
        public int? IdIngrediente { get; set; }
        public string? nombreIngrediente { get; set; }
        public string? detalleIngrediente { get; set; }
        public List<int>? IdsArticulos { get; set; }
    }
}