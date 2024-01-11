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
    public interface IEmployeeService
    {
        AckEntity<EmployeeModel> Update(EmployeeModel model);
        Ack Delete(int id);
        AckEntity<EmployeeModel> Crear(EmployeeModel model);
        EmployeeModel Obtener(int id);

        List<EmployeeModel> GetAll();
    }
}
