using Common.Entities;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Service
{
    public interface IStockService
    {
        AckEntity<StockModel> Update(StockModel model);
        Ack Delete(int id);
        AckEntity<StockModel> Crear(StockModel model);
        StockModel Obtener(int id);

        List<StockModel> GetAll();
    }
}
