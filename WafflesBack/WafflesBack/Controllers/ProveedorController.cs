using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackServices.Interfaces;

namespace WafflesBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        [Route("GetAllProveedores")]
        public async Task<IActionResult> GetAllProveedores()
        {
            try
            {
                var proveedores = await _proveedorService.GetAllProveedores();
                return Ok(proveedores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los proveedores: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddProveedor")]
        public async Task<IActionResult> AddProveedor([FromBody] ProveedorModel proveedor)
        {
            try
            {
                var result = await _proveedorService.AddProveedor(proveedor);
                if (result > 0)
                {
                    return Ok("Proveedor agregado correctamente");
                }
                else
                {
                    return BadRequest("Error al agregar proveedor");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar proveedor: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("UpdateProveedor/{id}")]
        public async Task<IActionResult> UpdateProveedor(int id, [FromBody] ProveedorModel proveedor)
        {
            try
            {
                proveedor.Id = id;
                var result = await _proveedorService.UpdateProveedor(proveedor);
                if (result > 0)
                {
                    return Ok($"Proveedor actualizado con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró proveedor con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar proveedor: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteProveedor/{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            try
            {
                var result = await _proveedorService.DeleteProveedor(id);
                if (result > 0)
                {
                    return Ok($"Proveedor eliminado con ID: {id}");
                }
                else
                {
                    return BadRequest($"No se encontró proveedor con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar proveedor: {ex.Message}");
            }
        }
    }
}