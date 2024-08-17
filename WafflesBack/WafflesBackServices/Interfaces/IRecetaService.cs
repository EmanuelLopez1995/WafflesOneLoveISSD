using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;


namespace WafflesBackServices.Interfaces 
{
    public interface IRecetaService
    {
        public Task<List<RecetaModel>> GetAllRecetas();
        public Task<int> AddReceta(RecetaModel receta);
        public Task<int> UpdateReceta(RecetaModel receta);
        public Task<int> DeleteReceta(int idReceta);
        public Task<RecetaModel> GetRecetaById(int idReceta);
    }
}