using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackServices;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UMDController : ControllerBase
    {
        private readonly IUMDService _umdService;

        public UMDController(IUMDService umdService)
        {
            _umdService = umdService;
        }

        [HttpGet]
        [Route("GetAllUMDs")]
        public async Task<IActionResult> GetAllUMDs()
        {
            try
            {
                return Ok(await _umdService.GetAllUMDs());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("AddUMD")]
        public async Task<IActionResult> AddUMD([FromBody] UMDModel umd)
        {
            try
            {
                var result = await _umdService.AddUMD(umd);
                if (result > 0)
                {
                    return Ok("Se agregó la unidad de medida");
                }
                else
                {
                    return BadRequest("Fallo al insertar la unidad de medida.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUMD/{id}")]
        public async Task<IActionResult> UpdateUMD(int id, [FromBody] UMDModel umd)
        {
            try
            {
                umd.idUMD = id;
                var result = await _umdService.UpdateUMD(umd);
                if (result > 0)
                {
                    return Ok($"Se actualizó la unidad de medida con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró la unidad de medida con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUMD/{id}")]
        public async Task<IActionResult> DeleteUMD(int id)
        {
            try
            {
                var result = await _umdService.DeleteUMD(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó la unidad de medida con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró la unidad de medida con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetUMDPorId/{id}")]
        public async Task<IActionResult> GetUMDPorId(int id)
        {
            try
            {
                var umd = await _umdService.GetUMDById(id);
                if (umd != null)
                {
                    return Ok(umd);
                }
                else
                {
                    return NotFound($"No se encontró la unidad de medida con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}