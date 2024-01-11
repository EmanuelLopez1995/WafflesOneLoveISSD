using System.Collections.Generic;
using System.Linq;
using Common.Interfaces.Service;
using Common.Model;
using Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace WafflesOneLove.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet(ApiRoutes.Employee.Get)]
        public IActionResult Get([FromQuery]int id)
        {
            var employee = employeeService.Obtener(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost(ApiRoutes.Employee.Post)]
        public IActionResult Post([FromBody] EmployeeModel model)
        {
            var ack = employeeService.Crear(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.Employee.Put)]
        public IActionResult Put([FromBody] EmployeeModel model)
        {
            var ack = employeeService.Update(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpDelete(ApiRoutes.Employee.Delete)]
        public IActionResult Delete([FromQuery] int id)
        {
            var ack = employeeService.Delete(id);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

         [HttpGet(ApiRoutes.Employee.GetAll)]
        public IActionResult GetAll()
        {
            var employee = employeeService.GetAll();
            if (employee == null || !employee.Any()) return NotFound();

            return Ok(employee);
        }

    }
}