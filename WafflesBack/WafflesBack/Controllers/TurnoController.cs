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

        [HttpGet]
        [Route("GetTurnoEnCurso")]
        public async Task<IActionResult> GetTurnoEnCurso()
        {
            try
            {
                var turnoEnCurso = await _turnoService.GetTurnoEnCurso();
                if (turnoEnCurso != null)
                {
                    return Ok(turnoEnCurso);
                }
                else
                {
                    return NotFound("No hay un turno en curso.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el turno en curso: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateTurnoEnCurso")]
        public async Task<IActionResult> UpdateTurnoEnCurso([FromBody] TurnoModel turno)
        {
            try
            {
                var result = await _turnoService.UpdateTurnoEnCurso(turno);
                if (result)
                {
                    return Ok("Turno actualizado correctamente");
                }
                else
                {
                    return BadRequest("No se pudo actualizar el turno.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el turno: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("FinalizarTurnoEnCurso")]
        public async Task<IActionResult> FinalizarTurnoEnCurso([FromBody] TurnoModel turno)
        {
            try
            {
                var result = await _turnoService.FinalizarTurnoEnCurso(turno);
                if (result)
                {
                    return Ok("Turno finalizado correctamente");
                }
                else
                {
                    return BadRequest("No se pudo finalizar el turno.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al finalizar el turno: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllTurnos")]
        public async Task<IActionResult> GetAllTurnos()
        {
            try
            {
                var turnos = await _turnoService.GetAllTurnos();
                if (turnos != null && turnos.Any())
                {
                    return Ok(turnos);
                }
                else
                {
                    return NotFound("No hay turnos disponibles.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los turnos: {ex.Message}");
            }
        }
    }
}