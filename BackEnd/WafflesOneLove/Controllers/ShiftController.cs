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
    public class ShiftController : Controller
    {
        private readonly IShiftService ShiftService;

        public ShiftController(IShiftService ShiftService)
        {
            this.ShiftService = ShiftService;
        }

        [HttpPost(ApiRoutes.Shift.Post)]
        public IActionResult Post([FromBody] ShiftModel model)
        {
            var ack = ShiftService.Create(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.Shift.Close)]
        public IActionResult Close([FromBody] ShiftModel model)
        {
            var ack = ShiftService.CloseShift(model);
            if (ack == null) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpGet(ApiRoutes.Shift.Get)]
        public IActionResult Get([FromQuery] ShiftModel model)
        {
            var ack = ShiftService.GetShift(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }


    }
}