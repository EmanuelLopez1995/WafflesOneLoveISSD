using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;


namespace WafflesBackServices.Services 
{
    
    
    public class RecetaService : IRecetaService
    {
        private readonly IRecetaRepository _recetaRepository;
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IIngredientePorRecetaRepository _ingredientePorRecetaRepository;

        public RecetaService(IRecetaRepository recetaRepository, IIngredienteRepository ingredienteRepository, IIngredientePorRecetaRepository ingredientePorRecetaRepository )
        {
            _ingredientePorRecetaRepository = ingredientePorRecetaRepository;
            _ingredienteRepository = ingredienteRepository;
            _recetaRepository = recetaRepository;
        }

        public async Task<List<RecetaModel>> GetAllRecetas()
        {
            try
            {
                // Obtener todas las recetas
                var listadoRecetas = await _recetaRepository.GetAllRecetas();

                // Para cada receta, obtener  los ingredientes
                foreach (var receta in listadoRecetas)
                {
                    var ingredientes = await _ingredientePorRecetaRepository.GetAllIngredientesPorReceta((int)receta.idReceta);
                    receta.Ingredientes = ingredientes;
                }

                return listadoRecetas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al obtener todas las recetas: {ex.Message}");
            }
        }

        public async Task<int> AddReceta(RecetaModel receta)
        {
            try
            {
                // Agregar la receta
                int idReceta = await _recetaRepository.AddReceta(receta);

                // Registrar la relación con los ingredientes
                if (receta.Ingredientes != null)
                {
                    foreach (var ingrediente in receta.Ingredientes)
                    {
                        // Registrar la relación ingrediente por receta
                        await _ingredientePorRecetaRepository.RegistrarIngredientePorReceta(idReceta, ingrediente.IdIngrediente, ingrediente.Cantidad);
                    }
                }

                return idReceta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al registrar receta: {ex.Message}");
            }
        }



        public async Task<int> UpdateReceta(RecetaModel receta)
        {
            try
            {
                // Actualizar la receta
                int idReceta = await _recetaRepository.UpdateReceta(receta);

                if (idReceta == null)
                {
                    throw new Exception("No se pudo actualizar la receta.");
                }

                // Obtener los ingredientes actuales de la receta
                var ingredientesActuales = await _ingredientePorRecetaRepository.GetAllIngredientesPorReceta((int)receta.idReceta);

                // Actualizar las relaciones con los ingredientes
                if (receta.Ingredientes != null)
                {
                    // Eliminar los ingredientes que ya no están en la receta
                    foreach (var ingredienteActual in ingredientesActuales)
                    {
                        if (!receta.Ingredientes.Any(i => i.IdIngrediente == ingredienteActual.IdIngrediente))
                        {
                            await _ingredientePorRecetaRepository.DeleteIngredientePorReceta((int)receta.idReceta, ingredienteActual.IdIngrediente);
                        }
                    }

                    // Añadir o actualizar los ingredientes de la receta
                    foreach (var ingrediente in receta.Ingredientes)
                    {
                        var ingredienteActual = ingredientesActuales.FirstOrDefault(i => i.IdIngrediente == ingrediente.IdIngrediente);
                        if (ingredienteActual == null)
                        {
                            // Si el ingrediente no está en la receta, agregar la relación
                            await _ingredientePorRecetaRepository.RegistrarIngredientePorReceta((int)receta.idReceta, ingrediente.IdIngrediente, ingrediente.Cantidad);
                        }
                        else if (ingredienteActual.Cantidad != ingrediente.Cantidad)
                        {
                            // Si el ingrediente está en la receta pero la cantidad es diferente, actualizar la cantidad
                            await _ingredientePorRecetaRepository.ActualizarCantidadIngredientePorReceta((int)receta.idReceta, ingrediente.IdIngrediente, ingrediente.Cantidad);
                        }
                    }
                }

                return idReceta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al actualizar receta: {ex.Message}");
            }
        }

        public async Task<int> DeleteReceta(int idReceta)
        {
            try
            {
                // Obtener los ingredientes asociados a la receta antes de eliminarla
                var ingredientesAsociados = await _ingredientePorRecetaRepository.GetAllIngredientesPorReceta(idReceta);

                // Eliminar las relaciones con los ingredientes asociados
                foreach (var ingrediente in ingredientesAsociados)
                {
                    await _ingredientePorRecetaRepository.DeleteIngredientePorReceta(idReceta, ingrediente.IdIngrediente);
                }

                // Eliminar la receta
                int resultado = await _recetaRepository.DeleteReceta(idReceta);

                if (resultado == 0)
                {
                    throw new Exception("No se pudo eliminar la receta.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al eliminar receta: {ex.Message}");
            }
        }

        public async Task<RecetaModel> GetRecetaById(int idReceta)
        {
            try
            {
                // Obtener la receta por su ID
                var receta = await _recetaRepository.GetRecetaById(idReceta);

                if (receta == null)
                {
                    throw new Exception("Receta no encontrada.");
                }

                // Obtener los ingredientes de la receta
                var ingredientes = await _ingredientePorRecetaRepository.GetAllIngredientesPorReceta(idReceta);
                receta.Ingredientes = ingredientes;

                return receta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al obtener la receta por ID: {ex.Message}");
            }
        }
    }
}