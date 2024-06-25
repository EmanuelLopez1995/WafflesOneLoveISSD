﻿using System;
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

        public async Task<List<TurnoEmpleadoModel>> ObtenerEmpleadosPorTurno(int idTurno)
        {
            var query = @"SELECT idEmpleado, horaIngresoEmpleado, descripcionIngreso, horaEgresoEmpleado, 
                         descripcionEgreso, idTurno, esRespDeApertCaja, esRespDeCierreCaja, sueldoTotalDelDia
                  FROM TurnoEmpleado
                  WHERE idTurno = @idTurno";

            var empleados = new List<TurnoEmpleadoModel>();

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idTurno", idTurno);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var empleado = new TurnoEmpleadoModel
                            {
                                idEmpleado = reader.IsDBNull(reader.GetOrdinal("idEmpleado")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idEmpleado")),
                                idTurno = reader.IsDBNull(reader.GetOrdinal("idTurno")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idTurno")),
                                horaIngresoEmpleado = reader.IsDBNull(reader.GetOrdinal("horaIngresoEmpleado")) ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("horaIngresoEmpleado")),
                                descripcionIngreso = reader.IsDBNull(reader.GetOrdinal("descripcionIngreso")) ? null : reader.GetString(reader.GetOrdinal("descripcionIngreso")),
                                horaEgresoEmpleado = reader.IsDBNull(reader.GetOrdinal("horaEgresoEmpleado")) ? (TimeSpan?)null : reader.GetTimeSpan(reader.GetOrdinal("horaEgresoEmpleado")),
                                descripcionEgreso = reader.IsDBNull(reader.GetOrdinal("descripcionEgreso")) ? null : reader.GetString(reader.GetOrdinal("descripcionEgreso")),
                                esRespDeApertCaja = reader.IsDBNull(reader.GetOrdinal("esRespDeApertCaja")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("esRespDeApertCaja")),
                                esRespDeCierreCaja = reader.IsDBNull(reader.GetOrdinal("esRespDeCierreCaja")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("esRespDeCierreCaja")),
                                sueldoTotalDelDia = reader.IsDBNull(reader.GetOrdinal("sueldoTotalDelDia")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("sueldoTotalDelDia"))
                            };

                            empleados.Add(empleado);
                        }
                    }
                }
            }

            return empleados;
        }

        public async Task<bool> ActualizarEmpleadoTurnoEnCurso(TurnoEmpleadoModel empleado, int idTurno)
        {
            var query = @"UPDATE TurnoEmpleado 
                          SET horaIngresoEmpleado = @horaIngresoEmpleado, 
                              descripcionIngreso = @descripcionIngreso, 
                              esRespDeApertCaja = @esRespDeApertCaja
                          WHERE idEmpleado = @idEmpleado AND idTurno = @idTurno";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@horaIngresoEmpleado", empleado.horaIngresoEmpleado);
                    command.Parameters.AddWithValue("@descripcionIngreso", empleado.descripcionIngreso);
                    command.Parameters.AddWithValue("@esRespDeApertCaja", empleado.esRespDeApertCaja.HasValue ? (object)empleado.esRespDeApertCaja.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@idEmpleado", empleado.idEmpleado);
                    command.Parameters.AddWithValue("@idTurno", idTurno);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> EliminarEmpleadoTurno(int idEmpleado, int idTurno)
        {
            var query = @"DELETE FROM TurnoEmpleado 
                  WHERE idEmpleado = @idEmpleado AND idTurno = @idTurno";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    command.Parameters.AddWithValue("@idTurno", idTurno);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> ActualizarEmpleadoTurnoFinalizado(TurnoEmpleadoModel empleado, int idTurno)
        {
            var query = @"UPDATE TurnoEmpleado 
                          SET horaEgresoEmpleado = @horaEgresoEmpleado, 
                              descripcionEgreso = @descripcionEgreso, 
                              esRespDeCierreCaja = @esRespDeCierreCaja,
                              sueldoTotalDelDia = @sueldoTotalDelDia
                          WHERE idEmpleado = @idEmpleado AND idTurno = @idTurno";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@horaEgresoEmpleado", empleado.horaEgresoEmpleado);
                    command.Parameters.AddWithValue("@descripcionEgreso", empleado.descripcionEgreso);
                    command.Parameters.AddWithValue("@esRespDeCierreCaja", empleado.esRespDeCierreCaja.HasValue ? (object)empleado.esRespDeCierreCaja.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@sueldoTotalDelDia", empleado.sueldoTotalDelDia);
                    command.Parameters.AddWithValue("@idEmpleado", empleado.idEmpleado);
                    command.Parameters.AddWithValue("@idTurno", idTurno);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}