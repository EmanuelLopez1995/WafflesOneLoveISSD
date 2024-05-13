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
    public class PaymentBoxInitialActiveController : Controller
    {
        private readonly IPaymentBoxInitialActiveService paymentBoxInitialActiveService;

        public PaymentBoxInitialActiveController(IPaymentBoxInitialActiveService paymentBoxInitialActiveService)
        {
            this.paymentBoxInitialActiveService = paymentBoxInitialActiveService;
        }

        [HttpGet(ApiRoutes.PaymentBoxInitialActive.Get)]
        public IActionResult Get([FromQuery]int id)
        {
            var paymentBoxInitialActive = paymentBoxInitialActiveService.Obtener(id);
            if (paymentBoxInitialActive == null) return NotFound();

            return Ok(paymentBoxInitialActive);
        }

        [HttpPost(ApiRoutes.PaymentBoxInitialActive.Post)]
        public IActionResult Post([FromBody] PaymentBoxInitialActiveModel model)
        {
            var ack = paymentBoxInitialActiveService.Crear(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.PaymentBoxInitialActive.Put)]
        public IActionResult Put([FromBody] PaymentBoxInitialActiveModel model)
        {
            var ack = paymentBoxInitialActiveService.Update(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpDelete(ApiRoutes.PaymentBoxInitialActive.Delete)]
        public IActionResult Delete([FromQuery] int id)
        {
            var ack = paymentBoxInitialActiveService.Delete(id);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

         [HttpGet(ApiRoutes.PaymentBoxInitialActive.GetAll)]
        public IActionResult GetAll()
        {
            var paymentBoxInitialActive = paymentBoxInitialActiveService.GetAll();

            if (paymentBoxInitialActive == null) {
                return Ok(new List<PaymentBoxInitialActive>());
            };
            return Ok(paymentBoxInitialActive);
        }

    }
}