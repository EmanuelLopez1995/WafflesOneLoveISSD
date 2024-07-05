using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IDetalleCompraRepository
    {
        Task<List<DetalleCompraModel>> GetDetallesByCompraId(int idCompra);
        Task<int> AddDetalleCompra(DetalleCompraModel detalleCompra);
    }
}