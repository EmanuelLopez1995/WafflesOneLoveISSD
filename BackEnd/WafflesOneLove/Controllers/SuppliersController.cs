using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace WafflesOneLove.Controllers
{
    [ApiController]
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        [HttpGet(ApiRoutes.Suppliers.Get)]
        public IActionResult Get([FromQuery]int id)
        {
            var suppliers = suppliersService.Obtener(id);
            if (suppliers == null) return NotFound();

            return Ok(suppliers);
        }

        [HttpPost(ApiRoutes.Suppliers.Post)]
        public IActionResult Post([FromBody] SuppliersModel model)
        {
            var ack = suppliersService.Crear(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.Suppliers.Put)]
        public IActionResult Put([FromBody] SuppliersModel model)
        {
            var ack = suppliersService.Update(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpDelete(ApiRoutes.Suppliers.Delete)]
        public IActionResult Delete([FromQuery] int id)
        {
            var ack = suppliersService.Delete(id);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }


        [HttpGet(ApiRoutes.Suppliers.GetAll)]
        public IActionResult GetAll()
        {
            var suppliers = suppliersService.GetAll();
<<<<<<< Updated upstream
            if (suppliers == null)
            {
                return Ok(new List<Suppliers>());
            };
=======
            if (suppliers == null) 
            {
                return Ok(new List<Suppliers>());
            }
>>>>>>> Stashed changes

            return Ok(suppliers);
        }
    }
}