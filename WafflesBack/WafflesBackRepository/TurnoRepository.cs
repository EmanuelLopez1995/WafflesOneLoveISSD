using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public TurnoRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<int> IniciarTurno(TurnoModel turno, int idCaja)
        {
            var query = @"INSERT INTO Turno (tipoTurno, fechaTurno, horaDelInicio, notasInicio, esFeriado, idCaja, idEncargadoTurno)
                          VALUES (@tipoTurno, @fechaTurno, @horaDelInicio, @notasInicio, @esFeriado, @idCaja, @idEncargadoTurno);
                          SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tipoTurno", turno.tipoTurno);
                    command.Parameters.AddWithValue("@fechaTurno", turno.fechaTurno);
                    command.Parameters.AddWithValue("@horaDelInicio", turno.horaDelInicio);
                    command.Parameters.AddWithValue("@notasInicio", turno.notasInicio);
                    command.Parameters.AddWithValue("@esFeriado", turno.esFeriado);
                    command.Parameters.AddWithValue("@idCaja", idCaja);
                    command.Parameters.AddWithValue("@idEncargadoTurno", turno.idEncargadoTurno);

                    int idTurno = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idTurno;
                }
            }
        }

        public async Task<TurnoModel> ObtenerTurnoEnCurso()
        {
            var query = @"SELECT TOP 1 idTurno, tipoTurno, fechaTurno, horaDelInicio, horaCierre, notasInicio, notasCierre, esFeriado, idEncargadoTurno, idCaja 
                          FROM Turno 
                          WHERE horaCierre IS NULL 
                          ORDER BY fechaTurno DESC, horaDelInicio DESC";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TurnoModel
                            {
                                idTurno = reader.GetInt32(reader.GetOrdinal("idTurno")),
                                tipoTurno = reader.GetInt32(reader.GetOrdinal("tipoTurno")),
                                fechaTurno = reader.GetDateTime(reader.GetOrdinal("fechaTurno")),
                                horaDelInicio = reader.GetTimeSpan(reader.GetOrdinal("horaDelInicio")),
                                horaCierre = reader.IsDBNull(reader.GetOrdinal("horaCierre")) ? (TimeSpan?)null : reader.GetTimeSpan(reader.GetOrdinal("horaCierre")),
                                notasInicio = reader.IsDBNull(reader.GetOrdinal("notasInicio")) ? null : reader.GetString(reader.GetOrdinal("notasInicio")),
                                notasCierre = reader.IsDBNull(reader.GetOrdinal("notasCierre")) ? null : reader.GetString(reader.GetOrdinal("notasCierre")),
                                esFeriado = reader.GetBoolean(reader.GetOrdinal("esFeriado")),
                                idEncargadoTurno = reader.GetInt32(reader.GetOrdinal("idEncargadoTurno")),
                                idCaja = reader.IsDBNull(reader.GetOrdinal("idCaja")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idCaja")),
                                // Empleados y Caja se pueden cargar aquí si hay datos adicionales necesarios
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}