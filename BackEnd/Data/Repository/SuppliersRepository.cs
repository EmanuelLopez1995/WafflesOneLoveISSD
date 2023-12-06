using Common.Entities;
using Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SuppliersRepository : Repository<Suppliers>, ISuppliersRepository
    {
        public SuppliersRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public Suppliers GetSuppliers(int id)
        {


            return context.Suppliers.FirstOrDefault(x => x.Id == id);
        }
    }
}
 