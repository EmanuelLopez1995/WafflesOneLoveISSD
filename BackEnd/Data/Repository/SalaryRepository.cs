using Common.Entities;
using Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SalaryRepository : Repository<Salary>, ISalaryRepository
    {
        public SalaryRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public Salary Obtener(int id)
        {


            return context.Salary.FirstOrDefault(x => x.Id == id);
        }
    }
}
 