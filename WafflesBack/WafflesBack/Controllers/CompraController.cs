using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : BaseController
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        [Route("GetAllCompras")]
        public async Task<IActionResult> GetAllCompras()
        {
            try
            {
                return Ok(await _compraService.GetAllCompras());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("AddCompra")]
        public async Task<IActionResult> AddCompra([FromBody] CompraModel compra)
        {
            try
            {
                var result = await _compraService.AddCompra(compra);
                if (result > 0)
                {
                    return Ok("Se agregó la compra");
                }
                else
                {
                    return BadRequest("Fallo al insertar compra.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCompra/{id}")]
        public async Task<IActionResult> UpdateCompra(int id, [FromBody] CompraModel compra)
        {
            try
            {
                compra.IdCompra = id;
                var result = await _compraService.UpdateCompra(compra);
                if (result > 0)
                {
                    return Ok($"Se actualizó la compra con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró la compra con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCompra/{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            try
            {
                var result = await _compraService.DeleteCompra(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó la compra con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró la compra con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetCompraPorId/{id}")]
        public async Task<IActionResult> GetCompraPorId(int id)
        {
            try
            {
                var compra = await _compraService.GetCompraPorId(id);
                if (compra != null)
                {
                    return Ok(compra);
                }
                else
                {
                    return NotFound($"No se encontró la compra con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}