using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;

namespace Service.Services
{
    public class StockService : DataAccessAbstractService, IStockService
    {
        public StockService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AckEntity<StockModel> Crear(StockModel model)
        {
            var ack = new AckEntity<StockModel>();
          

            var stock = new Stock
            {
                ProductBrand = model.ProductBrand,
                ProductName = model.ProductName,
                ActualStock = model.ActualStock,
                MinimunStock = model.MinimunStock,
                UnitOfMeasurement = model.UnitOfMeasurement,
                Detail = model.Detail
               
            };

            UoW.Stock.Add(stock);
            UoW.Complete();

            model.Id = stock.Id;

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }

        public Ack Delete(int id)
        {
            var ack = new Ack();

            var stock = UoW.Stock.Obtener(id);
            if (stock == null)
            {
                ack.Mensaje = "El articulo a borrar no Existe";
                return ack;
            }

            UoW.Stock.Remove(stock);
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public StockModel Obtener(int id)
        {
            var stock = UoW.Stock.Obtener(id);
            if (stock == null)
                return null;

            return new StockModel
            {
                ProductName = stock.ProductName,
                ProductBrand = stock.ProductBrand,
                ActualStock = stock.ActualStock,
                MinimunStock = stock.MinimunStock,
                UnitOfMeasurement = stock.UnitOfMeasurement,
                Detail = stock.Detail
               


            };
        }

        public List<StockModel> GetAll()
        {
            var list = UoW.Stock.GetAll();
            return list.Select(stock => new StockModel
            {
                ProductName = stock.ProductName,
                ProductBrand = stock.ProductBrand,
                ActualStock = stock.ActualStock,
                MinimunStock = stock.MinimunStock,
                UnitOfMeasurement = stock.UnitOfMeasurement,
                Detail = stock.Detail

            }).ToList();
        }

        public AckEntity<StockModel> Update(StockModel model)
        {
            var ack = new AckEntity<StockModel>();

            var stock = UoW.Stock.Obtener(model.Id);
            if (stock == null)
            {
                ack.Mensaje = "El articulo a actualizar no Existe";
                return ack;
            }

            stock.ProductName = model.ProductName;
            stock.ProductBrand = model.ProductBrand;
            stock.ActualStock = model.ActualStock;
            stock.MinimunStock = model.MinimunStock;
            stock.UnitOfMeasurement = model.UnitOfMeasurement;
            stock.Detail = model.Detail;
          

            UoW.Complete();

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }
    }
}