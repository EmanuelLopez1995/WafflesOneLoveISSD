using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface IProveedorService
    {
        Task<List<ProveedorModel>> GetAllProveedores();
        Task<int> AddProveedor(ProveedorModel proveedor);
        Task<int> UpdateProveedor(ProveedorModel proveedor);
        Task<int> DeleteProveedor(int id);
    }
}