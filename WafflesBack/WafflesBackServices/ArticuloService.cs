using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;

        public ArticuloService(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        public async Task<List<ArticuloModel>> GetAllArticulo()
        {
            return await _articuloRepository.GetAllArticulo();
        }

        public async Task<int> AddArticulo(ArticuloModel articulo)
        {
            return await _articuloRepository.AddArticulo(articulo);
        }

        public async Task<int> UpdateArticulo(ArticuloModel articulo)
        {
            return await _articuloRepository.UpdateArticulo(articulo);
        }

        public async Task<int> DeleteArticulo(int id)
        {
            return await _articuloRepository.DeleteArticulo(id);
        }

        public async Task<ArticuloModel> GetArticuloPorId(int id)
        {
            return await _articuloRepository.GetArticuloPorId(id);
        }
    }
}