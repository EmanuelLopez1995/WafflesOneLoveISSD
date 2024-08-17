using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models; 
using WafflesBackServices.Interfaces; 

namespace YourNamespace.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetaController : ControllerBase
    {
        private readonly IRecetaService _recetaService;

        public RecetaController(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecetas()
        {
            try
            {
                var recetas = await _recetaService.GetAllRecetas();
                return Ok(recetas);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecetaById(int id)
        {
            try
            {
                var receta = await _recetaService.GetRecetaById(id);
                if (receta == null)
                {
                    return NotFound($"No se encontró la receta con ID: {id}");
                }
                return Ok(receta);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReceta([FromBody] RecetaModel receta)
        {
            try
            {
                var result = await _recetaService.AddReceta(receta);
                if (result > 0)
                {
                    return Ok("Se agregó la receta correctamente");
                }
                else
                {
                    return BadRequest("Fallo al insertar la receta.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceta(int id, [FromBody] RecetaModel receta)
        {
            try
            {
                receta.idReceta = id;
                var result = await _recetaService.UpdateReceta(receta);
                if (result > 0)
                {
                    return Ok($"Se actualizó la receta con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró la receta con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            try
            {
                var result = await _recetaService.DeleteReceta(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó la receta con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró la receta con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}