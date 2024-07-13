using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;
        private readonly IArticuloPorIngredienteRepository _articuloPorIngredienteRepository;
        private readonly IIngredienteRepository _ingredienteRepository;

        public ArticuloService(IArticuloRepository articuloRepository, IArticuloPorIngredienteRepository articuloPorIngredienteRepository, IIngredienteRepository ingredienteRepository)
        {
            _articuloRepository = articuloRepository;
            _articuloPorIngredienteRepository = articuloPorIngredienteRepository;
            _ingredienteRepository = ingredienteRepository;
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
                // Obtiene el ingrediente por su ID
                var articulo = await _articuloRepository.GetArticuloPorId(id);

                if (articulo != null)
                {
                    // Elimina los artículos asociados al ingrediente
                    await _articuloPorIngredienteRepository.DeleteIngredientePorArticulo((int)articulo.IdArticulo);

                    // Elimina el ingrediente
                    return await _articuloRepository.DeleteArticulo(id);
                }

            // Si el ingrediente no existe, devuelve un valor indicando que no se hizo ninguna eliminación

            return 0;
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