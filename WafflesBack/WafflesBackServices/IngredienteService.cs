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
                int IdIngrediente = await _ingredienteRepository.UpdateIngrediente(ingrediente);

                if (ingrediente.IdsArticulos != null)
                {
                    await _articuloPorIngredienteRepository.DeleteArticulosPorIngrediente((int)ingrediente.IdIngrediente);

                    foreach (var idArticulo in ingrediente.IdsArticulos)
                    {
                        
                        await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente(idArticulo, (int)ingrediente.IdIngrediente);
                    }
                }

                return IdIngrediente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al registrar ingrediente: {ex.Message}");
            }
        }

        public async Task<int> DeleteIngrediente(int id)
        {
            // Obtiene el ingrediente por su ID
            var ingrediente = await _ingredienteRepository.GetIngredienteById(id);

            if (ingrediente != null)
            {
                // Elimina los artículos asociados al ingrediente
                await _articuloPorIngredienteRepository.DeleteArticulosPorIngrediente((int)ingrediente.IdIngrediente);

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

 
