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
                // Obtener la lista actual de empleados en el turno
                var empleadosActuales = await _turnoEmpleadoRepository.ObtenerEmpleadosPorTurno((int)turno.idTurno);

                // Identificar empleados eliminados y agregados
                var empleadosEliminados = empleadosActuales.Where(ea => !turno.Empleados.Any(te => te.idEmpleado == ea.idEmpleado)).ToList();
                var empleadosAgregados = turno.Empleados.Where(te => !empleadosActuales.Any(ea => ea.idEmpleado == te.idEmpleado)).ToList();

                // Actualizar el turno
                bool turnoUpdated = await _turnoRepository.ActualizarTurnoEnCurso(turno);
                if (!turnoUpdated)
                {
                    return false;
                }

                // Actualizar la caja
                bool cajaUpdated = await _cajaRepository.ActualizarCajaEnCurso(turno.Caja, (int)turno.idTurno);
                if (!cajaUpdated)
                {
                    return false;
                }

                // Eliminar empleados eliminados
                foreach (var empleado in empleadosEliminados)
                {
                    bool empleadoEliminado = await _turnoEmpleadoRepository.EliminarEmpleadoTurno(empleado.idEmpleado.Value, (int)turno.idTurno);
                    if (!empleadoEliminado)
                    {
                        return false;
                    }
                }

                // Agregar empleados agregados
                foreach (var empleado in empleadosAgregados)
                {
                    empleado.idTurno = turno.idTurno;
                    await _turnoEmpleadoRepository.RegistrarEmpleadoTurno(empleado);
                }

                // Actualizar empleados restantes
                foreach (var empleado in turno.Empleados)
                {
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

        public async Task<bool> FinalizarTurnoEnCurso(TurnoModel turno)
        {
            try
            {
                
                // Marcar el turno como finalizado
                bool turnoFinalizado = await _turnoRepository.FinalizarTurnoEnCurso(turno);
                if (!turnoFinalizado)
                {
                    return false;
                }

                // Actualizar la caja como finalizada, va a tomar el id de turno como id de caja
                bool cajaFinalizada = await _cajaRepository.FinalizarCajaEnCurso(turno.Caja, (int)turno.idTurno);
                if (!cajaFinalizada)
                {
                    return false;
                }

                // Actualizar empleados en el turno
                foreach (var empleado in turno.Empleados)
                {
                    bool empleadoActualizado = await _turnoEmpleadoRepository.ActualizarEmpleadoTurnoFinalizado(empleado, (int)turno.idTurno);
                    if (!empleadoActualizado)
                    {
                        return false;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al finalizar el turno en curso: {ex.Message}");
            }
        }

        public async Task<List<TurnoModel>> GetAllTurnos()
        {
            try
            {
                var turnos = await _turnoRepository.ObtenerTodosLosTurnos();
                foreach (var turno in turnos)
                {
                    turno.Caja = await _cajaRepository.GetCajaPorId((int)turno.idCaja);
                    turno.Empleados = await _turnoEmpleadoRepository.ObtenerEmpleadosPorTurno((int)turno.idTurno);
                }
                return turnos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al obtener todos los turnos: {ex.Message}");
            }
        }
    }
}