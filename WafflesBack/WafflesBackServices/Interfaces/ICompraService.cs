using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface ICompraService
    {
        Task<List<CompraModel>> GetAllCompras();
        Task<int> AddCompra(CompraModel compra);
        Task<int> UpdateCompra(CompraModel compra);
        Task<int> DeleteCompra(int id);
        Task<CompraModel> GetCompraPorId(int id);
        Task<int> ActualizarStockPorActualizarCompras(DetalleCompraModel detalleNuevo);
        Task ActualizarStockPorEliminarDetalleCompra(DetalleCompraModel detalleEliminado);
    }
}