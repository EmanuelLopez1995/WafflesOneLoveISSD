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
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
 
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("GetAllUsuarios")]
        public async Task<IActionResult> GetAllUsuarios()
        {
            try
            {
                return Ok(await _usuarioService.GetAllUsuarios());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("AddUsuario")]
        public async Task<IActionResult> AddUsuario([FromBody] UsuarioModel usuario)
        {
            try
            {
                var result = await _usuarioService.AddUsuario(usuario);
                if (result > 0)
                {
                    return Ok("Se agregó el usuario");
                }
                else
                {
                    return BadRequest("Fallo al insertar usuario.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            try
            {
                usuario.idUsuario = id;
                var result = await _usuarioService.UpdateUsuario(usuario);
                if (result > 0)
                {
                    return Ok($"Se actualizó el usuario con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el usuario con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUsuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var result = await _usuarioService.DeleteUsuario(id);
                if (result > 0)
                {
                    return Ok($"Se eliminó el usuario con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró el usuario con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetUsuarioPorId/{id}")]
        public async Task<IActionResult> GetUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioPorId(id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound($"No se encontró el usuario con ID: {id}");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}