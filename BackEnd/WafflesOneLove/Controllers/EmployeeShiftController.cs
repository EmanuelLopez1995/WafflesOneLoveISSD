using System.Collections.Generic;
using System.Linq;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace WafflesOneLove.Controllers
{
    [ApiController]
    public class EmployeeShiftController : Controller
    {
        private readonly IEmployeeShiftService employeeShiftService;

        public EmployeeShiftController(IEmployeeShiftService employeeShiftService)
        {
            this.employeeShiftService = employeeShiftService;
        }

        [HttpPost(ApiRoutes.EmployeeShift.Post)]
        public IActionResult Post([FromBody]EmployeeShiftModel model)
        {
            var ack = employeeShiftService.Create(model);
            if (ack == null) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.EmployeeShift.Close)]
        public IActionResult Close([FromBody] EmployeeShiftModel model)
        {
            var ack = employeeShiftService.CloseShift(model);
            if (ack == null) return BadRequest(ack);

            return Ok(ack);
        }
    }
}