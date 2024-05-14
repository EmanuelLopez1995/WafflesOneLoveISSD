
namespace Common.Model
{
    public class StockModel
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