using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface IEmpleadoService
    {
        public Task<List<EmpleadoModel>> GetAllEmpleados();
        public Task<int> AddEmpleado(EmpleadoModel empleado);
        public Task<int> UpdateEmpleado(EmpleadoModel empleado);
        public Task<int> DeleteEmpleado(int id);


    }
}
