using System;

namespace WafflesBackCommon.Models 
{
    public class RecetaModel
    {
        public int? idReceta { get; set; }
        public string nombreReceta { get; set; }
        public string procedimiento { get; set; }
        public List<IngredientePorRecetaModel> Ingredientes { get; set; }
    }
}