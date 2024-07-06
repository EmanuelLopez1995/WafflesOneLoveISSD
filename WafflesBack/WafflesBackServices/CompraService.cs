using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackCommon.Models;
using WafflesBackRepository.Repositories;

namespace WafflesBackServices
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IDetalleCompraRepository _detalleCompraRepository;


        public CompraService(ICompraRepository compraRepository, IDetalleCompraRepository detalleCompraRepository, IConfiguration configuration)
        {
            _compraRepository = compraRepository;
            _detalleCompraRepository = detalleCompraRepository;
        }

        public async Task<List<CompraModel>> GetAllCompras()
        {
            try
            {
                var compras = await _compraRepository.GetAllCompras();

                foreach (var compra in compras)
                {
                    compra.DetallesCompra = await _detalleCompraRepository.GetDetallesByCompraId((int)compra.IdCompra);                   
                }

                return compras;

            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener todas las compras");
            }
        }

        public async Task<int> AddCompra(CompraModel compra)
        {
            try
            {
                int idCompra = await _compraRepository.AddCompra(compra);

                foreach (var articulo in compra.DetallesCompra)
                {
                    articulo.IdCompra = idCompra;
                    await _detalleCompraRepository.AddDetalleCompra(articulo);
                }

                return idCompra;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al insertar la compra");
            }
        }

        public async Task<int> UpdateCompra(CompraModel compra)
        {
            try
            {
                return await _compraRepository.UpdateCompra(compra);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar la compra");
            }
        }

        public async Task<int> DeleteCompra(int id)
        {
            try
            {
                await _detalleCompraRepository.DeleteDetalleCompraPorIdCompra(id);
                return await _compraRepository.DeleteCompra(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al eliminar la compra");
            }
        }

        public async Task<CompraModel> GetCompraPorId(int id)
        {
            try
            {
                return await _compraRepository.GetCompraPorId(id);
            }
            catch (Exception)
            {
                throw new ApplicationException($"Error al obtener la compra con ID: {id}");
            }
        }
    }
}