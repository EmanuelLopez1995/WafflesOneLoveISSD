namespace WafflesBackCommon.Models
{
    public class IngredientePorRecetaModel
    {
        public int IdIngPorReceta { get; set; }
        public decimal Cantidad { get; set; }
        public int IdIngrediente { get; set; }
        public int IdReceta { get; set; }

       
    }
}