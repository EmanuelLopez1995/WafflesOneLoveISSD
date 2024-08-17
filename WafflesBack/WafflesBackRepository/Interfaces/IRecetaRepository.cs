using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces 
{
    public interface IRecetaRepository
    {
        
        Task<int> AddReceta(RecetaModel receta);
        Task<int> UpdateReceta(RecetaModel receta);
        Task<int> DeleteReceta(int idReceta);
        Task<List<RecetaModel>> GetAllRecetas();
        Task<RecetaModel> GetRecetaById(int idReceta);
    }
}