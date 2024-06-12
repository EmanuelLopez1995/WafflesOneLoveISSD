using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivoInicialController : ControllerBase
    {
        private readonly IActivoInicialService _activoInicialService;

        public ActivoInicialController(IActivoInicialService activoInicialService)
        {
            _activoInicialService = activoInicialService;
        }

        [HttpGet]
        [Route("GetActivoInicial/{id}")]
        public async Task<IActionResult> GetActivoInicial(int id)
        {
            try
            {
                var activoInicial = await _activoInicialService.GetActivoInicial(id);
                if (activoInicial != null)
                {
                    return Ok(activoInicial);
                }
                else
                {
                    return NotFound($"No se encontró el activo inicial con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateActivoInicial/{id}")]
        public async Task<IActionResult> UpdateActivoInicial(int id, [FromBody] ActivoInicialModel activoInicial)
        {
            try
            {
                activoInicial.IdActivoInicial = id;
                var result = await _activoInicialService.UpdateActivoInicial(activoInicial);
                if (result > 0)
                {
                    return Ok($"Se actualizó el activo inicial con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el activo inicial con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}