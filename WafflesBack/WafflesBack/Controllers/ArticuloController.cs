using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;
using System.Threading.Tasks;
using System;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticuloController : BaseController
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        [Route("GetAllArticulo")]
        public async Task<IActionResult> GetAllArticulo()
        {
            try
            {
                return Ok(await _articuloService.GetAllArticulo());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("AddArticulo")]
        public async Task<IActionResult> AddArticulo([FromBody] ArticuloModel articulo)
        {
            try
            {
                var result = await _articuloService.AddArticulo(articulo);
                if (result > 0)
                {
                    return Ok("Se agregó el artículo");
                }
                else
                {
                    return BadRequest("Fallo al insertar artículo.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateArticulo/{id}")]
        public async Task<IActionResult> UpdateArticulo(int id, [FromBody] ArticuloModel articulo)
        {
            try
            {
                articulo.IdArticulo = id;
                var result = await _articuloService.UpdateArticulo(articulo);
                if (result > 0)
                {
                    return Ok($"Se actualizó el artículo con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el artículo con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteArticulo/{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            try
            {
                var result = await _articuloService.DeleteArticulo(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó el artículo con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el artículo con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetArticuloPorId/{id}")]
        public async Task<IActionResult> GetArticuloPorId(int id)
        {
            try
            {
                var articulo = await _articuloService.GetArticuloPorId(id);
                if (articulo != null)
                {
                    return Ok(articulo);
                }
                else
                {
                    return NotFound($"No se encontró el artículo con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}