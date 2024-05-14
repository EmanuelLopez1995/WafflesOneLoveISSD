using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public int? ActualStock { get; set; }
        public int? MinimunStock { get; set; }
        public int? UnitOfMeasurement { get; set; }
        public string? Detail { get; set; }


    }
}
