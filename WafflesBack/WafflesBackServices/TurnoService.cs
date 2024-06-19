using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly ICajaRepository _cajaRepository;
        private readonly ITurnoEmpleadoRepository _turnoEmpleadoRepository;



        public TurnoService(ITurnoRepository turnoRepository, ICajaRepository cajaRepository, ITurnoEmpleadoRepository turnoEmpleadoRepository)
        {
            _turnoRepository = turnoRepository;
            _cajaRepository = cajaRepository;
            _turnoEmpleadoRepository = turnoEmpleadoRepository;
        }

        public async Task<int> IniciarTurno(TurnoModel turno)
        {
            try
            {
                int idCaja = await _cajaRepository.IniciarCaja(turno.Caja);
                int idTurno = await _turnoRepository.IniciarTurno(turno, idCaja);

                foreach (var empleado in turno.Empleados)
                {
                    empleado.idTurno = idTurno;
                    await _turnoEmpleadoRepository.RegistrarEmpleadoTurno(empleado);
                }

                return idTurno;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al iniciar el turno: {ex.Message}");
            }
        }

        public async Task<TurnoModel> GetTurnoEnCurso()
        {
            try
            {
                var turnoEnCurso = await _turnoRepository.ObtenerTurnoEnCurso();
                var cajaEnCurso = await _cajaRepository.GetCajaPorId((int)turnoEnCurso.idCaja);
                var empleadosTurno = await _turnoEmpleadoRepository.ObtenerEmpleadosPorTurno((int)turnoEnCurso.idTurno);
                turnoEnCurso.Caja = cajaEnCurso;
                turnoEnCurso.Empleados = empleadosTurno;
                return turnoEnCurso;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al obtener el turno en curso: {ex.Message}");
            }
        }

        public async Task<bool> UpdateTurnoEnCurso(TurnoModel turno)
        {
            try
            {
                bool turnoUpdated = await _turnoRepository.ActualizarTurnoEnCurso(turno);
                if (!turnoUpdated)
                {
                    return false;
                }

                bool cajaUpdated = await _cajaRepository.ActualizarCajaEnCurso(turno.Caja, (int)turno.idTurno);
                if (!cajaUpdated)
                {
                    return false;
                }

                foreach (var empleado in turno.Empleados)
                {
                    empleado.idTurno = turno.idTurno;
                    bool empleadoUpdated = await _turnoEmpleadoRepository.ActualizarEmpleadoTurnoEnCurso(empleado, (int)turno.idTurno);
                    if (!empleadoUpdated)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al actualizar el turno en curso: {ex.Message}");
            }
        }
    }
}