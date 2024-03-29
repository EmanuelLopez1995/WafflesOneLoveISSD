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
    public class SalaryController : Controller
    {
        private readonly ISalaryService salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }

        [HttpGet(ApiRoutes.Salary.Get)]
        public IActionResult Get([FromQuery]int id)
        {
            var salary = salaryService.Obtener(id);
            if (salary == null) return NotFound();

            return Ok(salary);
        }

        [HttpPost(ApiRoutes.Salary.Post)]
        public IActionResult Post([FromBody] SalaryModel model)
        {
            var ack = salaryService.Crear(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.Salary.Put)]
        public IActionResult Put([FromBody] SalaryModel model)
        {
            var ack = salaryService.Update(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpDelete(ApiRoutes.Salary.Delete)]
        public IActionResult Delete([FromQuery] int id)
        {
            var ack = salaryService.Delete(id);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

         [HttpGet(ApiRoutes.Salary.GetAll)]
        public IActionResult GetAll()
        {
            var salary = salaryService.GetAll();
            if (salary == null) {
                return Ok(new List<Salary>());
            };
            return Ok(salary);
        }

    }
}