using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<List<EmpleadoModel>> GetAllEmpleados();
        Task<int> AddEmpleado(EmpleadoModel model);
        Task<int> UpdateEmpleado(EmpleadoModel empleado);
        Task<int> DeleteEmpleado(int id);
    }
}