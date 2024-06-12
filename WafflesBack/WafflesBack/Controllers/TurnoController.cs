using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpPost]
        [Route("IniciarTurno")]
        public async Task<IActionResult> IniciarTurno([FromBody] TurnoModel turno)
        {
            try
            {
                var result = await _turnoService.IniciarTurno(turno);
                if (result > 0)
                {
                    return Ok($"Turno iniciado correctamente");
                }
                else
                {
                    return BadRequest($"No se pudo iniciar el turno.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al iniciar el turno: {ex.Message}");
            }
        }
    }
}