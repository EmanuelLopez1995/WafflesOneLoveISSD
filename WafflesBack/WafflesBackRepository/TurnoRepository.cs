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
                            ORDER BY idTurno DESC;";

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

        public async Task<bool> ActualizarTurnoEnCurso(TurnoModel turno)
        {
            var query = @"UPDATE Turno 
                          SET tipoTurno = @tipoTurno, 
                              fechaTurno = @fechaTurno, 
                              horaDelInicio = @horaDelInicio, 
                              notasInicio = @notasInicio, 
                              esFeriado = @esFeriado,
                              idEncargadoTurno = @idEncargadoTurno 
                          WHERE idTurno = @idTurno";

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
                    command.Parameters.AddWithValue("@idEncargadoTurno", turno.idEncargadoTurno);
                    command.Parameters.AddWithValue("@idTurno", turno.idTurno);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> FinalizarTurnoEnCurso(TurnoModel turno)
        {
            var query = @"UPDATE Turno 
                          SET horaCierre = @horaCierre, 
                              notasCierre = @notasCierre
                          WHERE idTurno = @idTurno";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@horaCierre", turno.horaCierre);
                    command.Parameters.AddWithValue("@notasCierre", turno.notasCierre);
                    command.Parameters.AddWithValue("@idTurno", turno.idTurno);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<TurnoModel>> ObtenerTodosLosTurnos()
        {
            var query = @"SELECT idTurno, tipoTurno, fechaTurno, horaDelInicio, horaCierre, notasInicio, notasCierre, esFeriado, idEncargadoTurno, idCaja 
                          FROM Turno 
                          ORDER BY idTurno DESC;";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var turnos = new List<TurnoModel>();
                        while (await reader.ReadAsync())
                        {
                            var turno = new TurnoModel
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
                            };
                            turnos.Add(turno);
                        }
                        return turnos;
                    }
                }
            }
        }
    }
}