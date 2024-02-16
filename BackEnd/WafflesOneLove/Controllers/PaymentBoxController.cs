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
    public class PaymentBoxController : Controller
    {
        private readonly IPaymentBoxService paymentBoxService;

        public PaymentBoxController(IPaymentBoxService paymentBoxService)
        {
            this.paymentBoxService = paymentBoxService;
        }

        [HttpGet(ApiRoutes.PaymentBox.Get)]
        public IActionResult Get([FromQuery]int id)
        {
            var paymentBox = paymentBoxService.Obtener(id);
            if (paymentBox == null) return NotFound();

            return Ok(paymentBox);
        }

        [HttpPost(ApiRoutes.PaymentBox.Post)]
        public IActionResult Post([FromBody] PaymentBoxModel model)
        {
            var ack = paymentBoxService.Crear(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.PaymentBox.Put)]
        public IActionResult Put([FromBody] PaymentBoxModel model)
        {
            var ack = paymentBoxService.Update(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpDelete(ApiRoutes.PaymentBox.Delete)]
        public IActionResult Delete([FromQuery] int id)
        {
            var ack = paymentBoxService.Delete(id);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

         [HttpGet(ApiRoutes.PaymentBox.GetAll)]
        public IActionResult GetAll()
        {
            var paymentBox = paymentBoxService.GetAll();

            if (paymentBox == null) {
                return Ok(new List<PaymentBox>());
            };
            return Ok(paymentBox);
        }

    }
}