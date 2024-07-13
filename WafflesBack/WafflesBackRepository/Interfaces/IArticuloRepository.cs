using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IArticuloRepository
    {
        Task<List<ArticuloModel>> GetAllArticulo();
        Task<int> AddArticulo(ArticuloModel model);
        Task<int> UpdateArticulo(ArticuloModel articulo);
        Task<int> UpdateArticuloEsIngrediente(int idArticulo);
        Task<int> DeleteArticulo(int id);
        Task<ArticuloModel> GetArticuloPorId(int idArticulo);
        Task<int> setEsMatPriEnFalsePorId(int idArticulo);
    }
}