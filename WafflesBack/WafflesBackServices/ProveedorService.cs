using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackRepository;
using WafflesBackCommon.Models;

namespace WafflesBackServices
{
    public class ProveedorService : IProveedorService
    {
        public readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<List<ProveedorModel>> GetAllProveedores()
        {
            try
            {
                return await _proveedorRepository.GetAllProveedores();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener todos los proveedores");
            }
        }

        public async Task<int> AddProveedor(ProveedorModel proveedor)
        {
            try
            {
                return await _proveedorRepository.AddProveedor(proveedor);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al insertar el proveedor");
            }
        }

        public async Task<int> UpdateProveedor(ProveedorModel proveedor)
        {
            try
            {
                return await _proveedorRepository.UpdateProveedor(proveedor);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar el proveedor");
            }
        }

        public async Task<int> DeleteProveedor(int id)
        {
            try
            {
                return await _proveedorRepository.DeleteProveedor(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al eliminar el proveedor");
            }
        }
    }
}