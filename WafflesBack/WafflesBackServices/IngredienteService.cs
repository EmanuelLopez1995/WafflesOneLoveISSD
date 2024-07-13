using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackRepository.Repositories;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class IngredienteService : IIngredienteService

    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IArticuloPorIngredienteRepository _articuloPorIngredienteRepository;
        private readonly IArticuloRepository _articuloRepository;

        public IngredienteService(IIngredienteRepository ingredienteRepository, IArticuloPorIngredienteRepository articuloPorIngredienteRepository, IArticuloRepository articuloRepository)
        {
            _ingredienteRepository = ingredienteRepository;
            _articuloPorIngredienteRepository = articuloPorIngredienteRepository;
            _articuloRepository = articuloRepository;
        }

        public async Task<List<IngredienteModel>> GetAllIngredientes()
        {
            var listadoIngredientes = await _ingredienteRepository.GetAllIngredientes();

            foreach (var ingrediente in listadoIngredientes)
            {
                var articuloIds = await _articuloPorIngredienteRepository.GetArticulosPorIngredienteId((int)ingrediente.IdIngrediente);
                ingrediente.IdsArticulos = articuloIds;
            }

            return listadoIngredientes;
        }

        public async Task<int> AddIngrediente(IngredienteModel ingrediente)
        {
            try
            {
                int IdIngrediente = await _ingredienteRepository.AddIngrediente(ingrediente);

                if (ingrediente.IdsArticulos != null)
                {
                    foreach (var idArticulo in ingrediente.IdsArticulos)
                    {
                        // Actualiza el artículo asociado como materia prima
                        await _articuloRepository.UpdateArticuloEsIngrediente(idArticulo);
                        // Asocia el artículo con el ingrediente
                        await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente(idArticulo, IdIngrediente);
                    }
                }

                return IdIngrediente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al registrar ingrediente: {ex.Message}");
            }
        }

        public async Task<int> UpdateIngrediente(IngredienteModel ingrediente)
        {
            try
            {
                // Obtenemos los artículos asociados al ingrediente antes de la actualización
                var articulosXIngredienteOriginal = await _articuloPorIngredienteRepository.ObtenerArticulosPorIngrediente((int)ingrediente.IdIngrediente);

                // Extraer solo los IdArticulo de la lista original
                var idsArticulosOriginales = articulosXIngredienteOriginal
                    .Where(a => a.IdArticulo.HasValue)
                    .Select(a => a.IdArticulo.Value)
                    .ToList();

                var idsEliminados = idsArticulosOriginales.Except(ingrediente.IdsArticulos).ToList();

                foreach (var idEliminado in idsEliminados)
                {
                    // Actualizamos el esMateriaPrima a False y le ponemos peso 0, deja de ser Ingrediente 
                    // CHEQUEAR ESTO! QUE PASA SI TENEMOS EL MISMO ARTICULO EN 2 INGREDIENTES?
                    await _articuloRepository.setEsMatPriEnFalsePorId(idEliminado);
                }

                int IdIngrediente = await _ingredienteRepository.UpdateIngrediente(ingrediente);

                if (ingrediente.IdsArticulos != null)
                {
                    await _articuloPorIngredienteRepository.DeleteArticulosPorIngrediente((int)ingrediente.IdIngrediente);

                    foreach (var idArticulo in ingrediente.IdsArticulos)
                    {
                        await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente(idArticulo, (int)ingrediente.IdIngrediente);
                        await _articuloRepository.UpdateArticuloEsIngrediente(idArticulo);
                    }
                }

                return IdIngrediente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al actualizar ingrediente: {ex.Message}");
            }
        }

        public async Task<int> DeleteIngrediente(int id)
        {
            // Obtiene el ingrediente por su ID
            var ingrediente = await GetIngredienteById(id);

            if (ingrediente != null)
            {
                // Elimina los artículos asociados al ingrediente
                await _articuloPorIngredienteRepository.DeleteArticulosPorIngrediente((int)ingrediente.IdIngrediente);

                //Esto va a setear materia prima en false para los articulos relacionados
                foreach (var idArticulo in ingrediente.IdsArticulos)
                {
                    await _articuloRepository.setEsMatPriEnFalsePorId(idArticulo);
                }

                // Elimina el ingrediente
                return await _ingredienteRepository.DeleteIngrediente(id);
            }

            // Si el ingrediente no existe, devuelve un valor indicando que no se hizo ninguna eliminación
            return 0;
        }
        public async Task<IngredienteModel> GetIngredienteById(int id)
        {
            
            var ingrediente = await _ingredienteRepository.GetIngredienteById(id);

            if (ingrediente != null)
            {
                
                var articuloIds = await _articuloPorIngredienteRepository.GetArticulosPorIngredienteId((int)ingrediente.IdIngrediente);
                ingrediente.IdsArticulos = articuloIds;
            }

            return ingrediente;
        }

    }
}

 
