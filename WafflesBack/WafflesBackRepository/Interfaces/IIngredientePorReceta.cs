using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IIngredientePorRecetaRepository
    {
        Task RegistrarIngredientePorReceta(int idReceta, int idIngrediente, decimal cantidad);
        Task DeleteIngredientePorReceta(int idReceta, int idIngrediente);
        Task<List<IngredientePorRecetaModel>> GetAllIngredientesPorReceta(int idReceta);
        Task<bool> ActualizarIngredientePorReceta(IngredientePorRecetaModel model);
        Task<bool> ActualizarCantidadIngredientePorReceta(int idReceta, int idIngrediente, decimal cantidad);
        Task<List<int>> GetIngredientesPorRecetaId(int idReceta);

        Task<int> GetRecetaPorIngredienteId(int idIngrediente);
    }
}