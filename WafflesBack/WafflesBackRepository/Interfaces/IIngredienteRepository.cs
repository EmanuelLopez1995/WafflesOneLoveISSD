using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IIngredienteRepository
    {
        Task<List<IngredienteModel>> GetAllIngredientes();
        Task<IngredienteModel> GetIngredienteById(int id);
        Task<int> AddIngrediente(IngredienteModel ingrediente);
        Task<int> UpdateIngrediente(IngredienteModel ingrediente);
        Task<int> DeleteIngrediente(int id);
    }
}