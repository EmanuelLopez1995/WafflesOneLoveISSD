using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ICompraRepository
    {
        Task<List<CompraModel>> GetAllCompras();
        Task<int> AddCompra(CompraModel compra);
        Task<int> UpdateCompra(CompraModel compra);
        Task<int> DeleteCompra(int id);
        Task<CompraModel> GetCompraPorId(int id);
    }
}