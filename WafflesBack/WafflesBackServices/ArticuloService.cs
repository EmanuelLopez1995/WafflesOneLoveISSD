using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly IArticuloPorIngredienteRepository _articuloPorIngredienteRepository;
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IDetalleCompraRepository _detalleCompraRepository;


        public ArticuloService(IArticuloRepository articuloRepository, IArticuloPorIngredienteRepository articuloPorIngredienteRepository, IIngredienteRepository ingredienteRepository, IDetalleCompraRepository detalleCompraRepository)
        {
            _articuloRepository = articuloRepository;
            _articuloPorIngredienteRepository = articuloPorIngredienteRepository;
            _ingredienteRepository = ingredienteRepository;
            _detalleCompraRepository = detalleCompraRepository;
        }

        public async Task<List<ArticuloModel>> GetAllArticulo()
        {

            var listadoArticulos = await _articuloRepository.GetAllArticulo();

            foreach (var articulo in listadoArticulos)
            {
                var ingredienteId = await _articuloPorIngredienteRepository.GetIngredientePorArticuloId((int)articulo.IdArticulo);
                articulo.IdIngrediente = ingredienteId;
            }   

            return listadoArticulos;
        }

        public async Task<int> AddArticulo(ArticuloModel articulo)
        {
            try
            {
                int IdArticulo = await _articuloRepository.AddArticulo(articulo);

                if (articulo.IdIngrediente != 0) 
                {
                    // Asocia el artículo con el ingrediente
                    await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente(IdArticulo, articulo.IdIngrediente);
                }

                return IdArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al registrar artículo: {ex.Message}");
            }
        }


        public async Task<int> UpdateArticulo(ArticuloModel articulo)
        {
            try
            {
                int IdArticulo = await _articuloRepository.UpdateArticulo(articulo);

                await _articuloPorIngredienteRepository.DeleteIngredientePorArticulo((int)articulo.IdArticulo); //Dejalo ahí

                if (articulo.esMateriaPrima)
                {
                    await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente((int)articulo.IdArticulo, articulo.IdIngrediente);
                }

                return IdArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al actualizar articulo: {ex.Message}");
            }
            
        }

        public async Task<int> DeleteArticulo(int id)
        {
            // Verificar si existen detalles de compra asociados al artículo
            var detallesCompra = await _detalleCompraRepository.GetDetallesByArticuloId(id);

            if (detallesCompra.Count > 0)
            {
                // Si hay detalles de compra asociados, no se puede eliminar el artículo
                throw new Exception("No se puede eliminar el artículo porque está asociado a registros de compra.");
            }

            // Si no hay detalles de compra asociados, proceder con la eliminación del artículo
            return await _articuloRepository.DeleteArticulo(id);
        }

        public async Task<ArticuloModel> GetArticuloPorId(int id)
        {

            var articulo = await _articuloRepository.GetArticuloPorId(id);

            if (articulo != null)
            {

                var ingredienteId = await _articuloPorIngredienteRepository.GetIngredientePorArticuloId((int)articulo.IdArticulo);
                articulo.IdIngrediente = ingredienteId;
            }

            return articulo;
        }
    }
}