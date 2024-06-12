using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : BaseController
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("GetAllEmpleados")]
        public async Task<IActionResult> GetAllEmpleados()
        {
            try
            {
                return Ok(await _empleadoService.GetAllEmpleados());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("AddEmpleado")]
        public async Task<IActionResult> AddEmpleado([FromBody] EmpleadoModel empleado)
        {
            try
            {
                var result = await _empleadoService.AddEmpleado(empleado);
                if (result > 0)
                {
                    return Ok("Se agregó el empleado");
                }
                else
                {
                    return BadRequest("Fallo al insertar empleado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEmpleado/{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, [FromBody] EmpleadoModel empleado)
        {
            try
            {
                empleado.idEmpleado = id;
                var result = await _empleadoService.UpdateEmpleado(empleado);
                if (result > 0)
                {
                    return Ok($"Se actualizó el empleado con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el empleado con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEmpleado/{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                var result = await _empleadoService.DeleteEmpleado(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó el empleado con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el empleado con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}

