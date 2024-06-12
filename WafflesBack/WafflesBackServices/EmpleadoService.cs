using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackRepository;
using WafflesBackCommon.Models;

namespace WafflesBackServices
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        public EmpleadoService(IEmpleadoRepository empleadoaRepository,
            IConfiguration configuration)
        {
            _empleadoRepository = empleadoaRepository;
        }

        public async Task<List<EmpleadoModel>> GetAllEmpleados()
        {
            try
            {
                return (await _empleadoRepository.GetAllEmpleados());
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener todos los empleados");
            }
        }

        public async Task<int> AddEmpleado(EmpleadoModel empleado)
        {
            try
            {
                return await _empleadoRepository.AddEmpleado(empleado);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al insertar el empleado");
            }
        }

        public async Task<int> UpdateEmpleado(EmpleadoModel empleado)
        {
            try
            {
                return await _empleadoRepository.UpdateEmpleado(empleado);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar el empleado");
            }
        }

        public async Task<int> DeleteEmpleado(int id)
        {
            try
            {
                return await _empleadoRepository.DeleteEmpleado(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al eliminar el empleado");
            }
        }
    }
}
