using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackCommon.Models;
using WafflesBackRepository.Repositories;
using System.Transactions;

namespace WafflesBackServices
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IDetalleCompraRepository _detalleCompraRepository;
        private readonly IArticuloRepository _articuloRepository;



        public CompraService(ICompraRepository compraRepository, IDetalleCompraRepository detalleCompraRepository, IConfiguration configuration, IArticuloRepository articuloRepository)
        {
            _compraRepository = compraRepository;
            _detalleCompraRepository = detalleCompraRepository;
            _articuloRepository = articuloRepository;
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

                foreach (var item in compra.DetallesCompra)
                {
                    item.IdCompra = idCompra;
                    await _detalleCompraRepository.AddDetalleCompra(item);

                    //actualizamos stock:
                    var articuloAEditar = await _articuloRepository.GetArticuloPorId((int)item.IdArticulo);
                    var stockActualArticulo = articuloAEditar.stockActual;
                    var valorTotal = stockActualArticulo + item.Cantidad;
                    articuloAEditar.stockActual = (decimal)valorTotal;
                    await _articuloRepository.UpdateArticulo(articuloAEditar);
                }

                return idCompra;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al insertar la compra");
            }
        }

        public async Task<int> ActualizarStockPorActualizarCompras(DetalleCompraModel detalleNuevo) {
            var articuloAEditar = await _articuloRepository.GetArticuloPorId((int)detalleNuevo.IdArticulo);
            var stockActualArticulo = articuloAEditar.stockActual;

            if (detalleNuevo.IdDetalleCompra != null)
            {
                //el articulo ya existia asi que modificamos
                //Obtenemos dato de cantidad actual del detalle de compra
                var detalleCompra = await _detalleCompraRepository.GetDetallesByDetallesId((int)detalleNuevo.IdDetalleCompra);
                var cantidadActual = detalleCompra.Cantidad;
                var cantidadNueva = detalleNuevo.Cantidad;
                var diferencia = cantidadActual - cantidadNueva;
                //Actualizamos el stockActual en articulo
                articuloAEditar.stockActual = stockActualArticulo - (decimal)diferencia;
                await _articuloRepository.UpdateArticulo(articuloAEditar);
            }
            else {
                //El articulo es nuevo y sumamos el stock 
                //var articuloAEditar = await _articuloRepository.GetArticuloPorId((int)detalleNuevo.IdArticulo);
                //var stockActualArticulo = articuloAEditar.stockActual;
                var valorTotal = stockActualArticulo + detalleNuevo.Cantidad;
                articuloAEditar.stockActual = (decimal)valorTotal;
                await _articuloRepository.UpdateArticulo(articuloAEditar);

            }

            return 1;
        }

        public async Task ActualizarStockPorEliminarDetalleCompra(DetalleCompraModel detalleEliminado)
        {
            var articulo = await _articuloRepository.GetArticuloPorId((int)detalleEliminado.IdArticulo);

            articulo.stockActual -= (int)detalleEliminado.Cantidad;

            await _articuloRepository.UpdateArticulo(articulo);
        }

        public async Task<int> UpdateCompra(CompraModel compra)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //Actualizar el stock de existentes o nuevos 
                    foreach (var detalleNuevo in compra.DetallesCompra)
                    {
                        await ActualizarStockPorActualizarCompras(detalleNuevo);
                    }
                    //Actualizar stock de eliminados
                    var arrayDetalleComprasActual = await _detalleCompraRepository.GetDetallesByCompraId((int)compra.IdCompra);
                    var idDetalleComprasActual = arrayDetalleComprasActual.Select(detalle => detalle.IdDetalleCompra).ToList();
                    var idDetalleComprasNuevos = compra.DetallesCompra.Select(detalle => detalle.IdDetalleCompra).ToList();
                    var idDetalleComprasEliminados = idDetalleComprasActual.Except(idDetalleComprasNuevos).ToList();

                    foreach (var idDetalleEliminado in idDetalleComprasEliminados)
                    {
                        var detalleEliminado = arrayDetalleComprasActual.FirstOrDefault(detalle => detalle.IdDetalleCompra == idDetalleEliminado);
                        if (detalleEliminado != null)
                        {
                            await ActualizarStockPorEliminarDetalleCompra(detalleEliminado);
                        }
                    }

                    // Actualizar la compra
                    await _compraRepository.UpdateCompra(compra);

                    // Eliminar todos los detalles existentes de la compra
                    await _detalleCompraRepository.DeleteDetalleCompraPorIdCompra((int)compra.IdCompra);

                    // Insertar los nuevos detalles
                    foreach (var detalleNuevo in compra.DetallesCompra)
                    {
                        detalleNuevo.IdCompra = compra.IdCompra;
                        await _detalleCompraRepository.AddDetalleCompra(detalleNuevo);
                    }

                    transaction.Complete();
                    return (int)compra.IdCompra;
                }
                catch (Exception)
                {
                    transaction.Dispose();
                    throw new ApplicationException("Error al actualizar la compra");
                }
            }
        }

        public async Task<int> DeleteCompra(int id)
        {
            try
            {
                //Descontamos el stock de los detalles
                var detallesCompra = await _detalleCompraRepository.GetDetallesByCompraId((int)id);
                foreach (var detalle in detallesCompra)
                {
                     await ActualizarStockPorEliminarDetalleCompra(detalle);
                }

                //Eliminamos registros de compras
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