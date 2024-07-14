using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IDetalleCompraRepository
    {
        Task<List<DetalleCompraModel>> GetDetallesByCompraId(int idCompra);
        Task<List<DetalleCompraModel>> GetDetallesByArticuloId(int idArticulo);
        Task<int> AddDetalleCompra(DetalleCompraModel detalleCompra);
        Task<int> DeleteDetalleCompraPorIdCompra(int idCompra);
        Task<DetalleCompraModel> GetDetallesByDetallesId(int idDetalleCompra);
    }
}