using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository.Repositories
{
    public class TurnoEmpleadoRepository : ITurnoEmpleadoRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public TurnoEmpleadoRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task RegistrarEmpleadoTurno(TurnoEmpleadoModel empleado)
        {
            var query = @"INSERT INTO TurnoEmpleado (idEmpleado, horaIngresoEmpleado, descripcionIngreso, idTurno,
                                                      esRespDeApertCaja)
                          VALUES (@idEmpleado, @horaIngresoEmpleado, @descripcionIngreso,
                                  @idTurno,
                                  @esRespDeApertCaja);";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idEmpleado", empleado.idEmpleado);
                    command.Parameters.AddWithValue("@descripcionIngreso", empleado.descripcionIngreso);
                    command.Parameters.AddWithValue("@horaIngresoEmpleado", empleado.horaIngresoEmpleado);
                    command.Parameters.AddWithValue("@idTurno", empleado.idTurno);
                    command.Parameters.AddWithValue("@esRespDeApertCaja", empleado.esRespDeApertCaja);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}