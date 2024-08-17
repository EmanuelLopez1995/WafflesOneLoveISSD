using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IArticuloPorIngredienteRepository
    {
        Task RegistrarArticulosPorIngrediente(int idArticulo, int idIngrediente);
        Task DeleteArticulosPorIngrediente(int idIngrediente);
        Task DeleteIngredientePorArticulo(int idArticulo);
        Task<List<ArticuloPorIngredienteModel>> ObtenerArticulosPorIngrediente(int idIngrediente);
        Task<bool> ActualizarArticuloPorIngrediente(ArticuloPorIngredienteModel model);

        Task<List<int>> GetArticulosPorIngredienteId(int idIngrediente);
        Task<int> GetIngredientePorArticuloId(int idArticulo);
    }
}