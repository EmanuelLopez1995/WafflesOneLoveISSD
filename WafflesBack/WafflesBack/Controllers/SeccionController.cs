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
    public class SeccionController : BaseController
    {
        private readonly ISeccionService _seccionService;

        public SeccionController(ISeccionService seccionService)
        {
            _seccionService = seccionService;
        }

        [HttpGet]
        [Route("GetAllSecciones")]
        public async Task<IActionResult> GetAllSecciones()
        {
            try
            {
                return Ok(await _seccionService.GetAllSecciones());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}