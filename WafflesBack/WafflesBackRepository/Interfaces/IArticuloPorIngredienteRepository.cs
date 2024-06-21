using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IArticuloPorIngredienteRepository
    {
        Task RegistrarArticulosPorIngrediente(ArticuloPorIngredienteModel model);
        Task<List<ArticuloPorIngredienteModel>> ObtenerArticulosPorIngrediente(int idIngrediente);
        Task<bool> ActualizarArticuloPorIngrediente(ArticuloPorIngredienteModel model);
    }
}