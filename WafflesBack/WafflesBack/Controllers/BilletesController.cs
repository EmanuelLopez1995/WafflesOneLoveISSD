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
    public class BilletesController : ControllerBase
    {
        private readonly IBilletesService _billetesService;

        public BilletesController(IBilletesService billetesService)
        {
            _billetesService = billetesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBilletes()
        {
            try
            {
                var billetes = await _billetesService.GetAllBilletes();
                return Ok(billetes);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBilleteById(int id)
        {
            try
            {
                var billete = await _billetesService.GetBilleteById(id);
                if (billete == null)
                {
                    return NotFound($"No se encontró el billete con ID: {id}");
                }
                return Ok(billete);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBillete([FromBody] BilleteModel billete)
        {
            try
            {
                var result = await _billetesService.AddBillete(billete);
                if (result > 0)
                {
                    return Ok("Se agregó el billete correctamente");
                }
                else
                {
                    return BadRequest("Fallo al insertar el billete.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillete(int id, [FromBody] BilleteModel billete)
        {
            try
            {
                billete.IdBillete = id;
                var result = await _billetesService.UpdateBillete(billete);
                if (result > 0)
                {
                    return Ok($"Se actualizó el billete con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el billete con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillete(int id)
        {
            try
            {
                var result = await _billetesService.DeleteBillete(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó el billete con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el billete con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
