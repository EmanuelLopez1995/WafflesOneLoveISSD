using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SueldosBasicosController : ControllerBase
    {
        private readonly ISueldosBasicosService _sueldosBasicosService;

        public SueldosBasicosController(ISueldosBasicosService sueldosBasicosService)
        {
            _sueldosBasicosService = sueldosBasicosService;
        }

        [HttpPut]
        [Route("UpdateSueldosBasicos/{id}")]
        public async Task<IActionResult> UpdateSueldosBasicos(int id, [FromBody] SueldosBasicosModel sueldosBasicos)
        {
            // id 1 - colaborador
            // id 2 - encargado
            // id 2 - dueño
            try
            {
                sueldosBasicos.idSueldosBasicos = id;
                var result = await _sueldosBasicosService.UpdateSueldosBasicos(sueldosBasicos);
                if (result > 0)
                {
                    return Ok($"SueldosBasicos actualizado con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró SueldosBasicos con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar SueldosBasicos: {ex.Message}");
            }
        }
    }
}