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
    public class StockController : Controller
    {
        private readonly IStockService stockService;

        public StockController(IStockService stockService)
        {
            this.stockService = stockService;
        }

        [HttpGet(ApiRoutes.Stock.Get)]
        public IActionResult Get([FromQuery]int id)
        {
            var stock = stockService.Obtener(id);
            if (stock == null) return NotFound();

            return Ok(stock);
        }

        [HttpPost(ApiRoutes.Stock.Post)]
        public IActionResult Post([FromBody] StockModel model)
        {
            var ack = stockService.Crear(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpPut(ApiRoutes.Stock.Put)]
        public IActionResult Put([FromBody] StockModel model)
        {
            var ack = stockService.Update(model);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

        [HttpDelete(ApiRoutes.Stock.Delete)]
        public IActionResult Delete([FromQuery] int id)
        {
            var ack = stockService.Delete(id);
            if (!ack.Exito) return BadRequest(ack);

            return Ok(ack);
        }

         [HttpGet(ApiRoutes.Stock.GetAll)]
        public IActionResult GetAll()
        {
            var stock = stockService.GetAll();
            if (stock == null) {
                return Ok(new List<Stock>());
            };
            return Ok(stock);
        }

    }
}