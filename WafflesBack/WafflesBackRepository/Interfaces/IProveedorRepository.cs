using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IProveedorRepository
    {
        Task<List<ProveedorModel>> GetAllProveedores();
        Task<int> AddProveedor(ProveedorModel proveedor);
        Task<int> UpdateProveedor(ProveedorModel proveedor);
        Task<int> DeleteProveedor(int id);
    }
}