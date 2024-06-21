using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteService _ingredienteService;

        public IngredienteController(IIngredienteService ingredienteService)
        {
            _ingredienteService = ingredienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredientes()
        {
            try
            {
                var ingredientes = await _ingredienteService.GetAllIngredientes();
                return Ok(ingredientes);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredienteById(int id)
        {
            try
            {
                var ingrediente = await _ingredienteService.GetIngredienteById(id);
                if (ingrediente == null)
                {
                    return NotFound($"No se encontró el ingrediente con ID: {id}");
                }
                return Ok(ingrediente);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIngrediente([FromBody] IngredienteModel ingrediente)
        {
            try
            {
                var result = await _ingredienteService.AddIngrediente(ingrediente);
                if (result > 0)
                {
                    return Ok("Se agregó el ingrediente correctamente");
                }
                else
                {
                    return BadRequest("Fallo al insertar el ingrediente.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngrediente(int id, [FromBody] IngredienteModel ingrediente)
        {
            try
            {
                ingrediente.IdIngrediente = id;
                var result = await _ingredienteService.UpdateIngrediente(ingrediente);
                if (result > 0)
                {
                    return Ok($"Se actualizó el ingrediente con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el ingrediente con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngrediente(int id)
        {
            try
            {
                var result = await _ingredienteService.DeleteIngrediente(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó el ingrediente con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el ingrediente con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
