using Common.Entities;
using Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public Stock Obtener(int id)
        {


            return context.Stock.FirstOrDefault(x => x.Id == id);
        }
    }
}
 