using Common.Entities;
using Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public Employee Obtener(int id)
        {


            return context.Employees.FirstOrDefault(x => x.Id == id);
        }
    }
}
 