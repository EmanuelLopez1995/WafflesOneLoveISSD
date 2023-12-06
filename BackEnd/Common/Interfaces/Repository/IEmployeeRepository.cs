using Common.Entities;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee Obtener(int id);
    }
}
