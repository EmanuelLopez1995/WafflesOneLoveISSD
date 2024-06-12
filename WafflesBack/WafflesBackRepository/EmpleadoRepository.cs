using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public EmpleadoRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<EmpleadoModel>> GetAllEmpleados()
        {
            var empleadoList = new List<EmpleadoModel>();
            var query = "SELECT * FROM Empleado";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var empleado = new EmpleadoModel
                            {
                                idEmpleado = reader.GetInt32(0),
                                nombreEmpleado = reader.GetString(1),
                                apellidoEmpleado = reader.GetString(2),
                                direccionEmpleado = reader.GetString(3),
                                telefonoEmpleado = reader.GetString(4),
                                mailEmpleado = reader.GetString(5),
                                DNIEmpleado = reader.GetString(6),
                                idPuestoEmpleado = reader.GetInt32(7),
                            };
                            empleadoList.Add(empleado);
                        }
                    }
                }
            }
            return empleadoList;
        }

        public async Task<int> AddEmpleado(EmpleadoModel empleado)
        {
            var query = @"INSERT INTO Empleado (nombreEmpleado, apellidoEmpleado, direccionEmpleado, 
                           telefonoEmpleado, mailEmpleado, DNIEmpleado, idPuestoEmpleado)
                          VALUES (@nombreEmpleado, @apellidoEmpleado, @direccionEmpleado, @telefonoEmpleado, 
                          @mailEmpleado, @DNIEmpleado, @idPuestoEmpleado)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreEmpleado", empleado.nombreEmpleado);
                    command.Parameters.AddWithValue("@apellidoEmpleado", empleado.apellidoEmpleado);
                    command.Parameters.AddWithValue("@direccionEmpleado", empleado.direccionEmpleado);
                    command.Parameters.AddWithValue("@telefonoEmpleado", empleado.telefonoEmpleado);
                    command.Parameters.AddWithValue("@mailEmpleado", empleado.mailEmpleado);
                    command.Parameters.AddWithValue("@DNIEmpleado", empleado.DNIEmpleado);
                    command.Parameters.AddWithValue("@idPuestoEmpleado", empleado.idPuestoEmpleado);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> UpdateEmpleado(EmpleadoModel empleado)
        {
            var query = @"UPDATE Empleado 
                          SET nombreEmpleado = @nombreEmpleado, apellidoEmpleado = @apellidoEmpleado, 
                              direccionEmpleado = @direccionEmpleado, telefonoEmpleado = @telefonoEmpleado, 
                              mailEmpleado = @mailEmpleado, DNIEmpleado = @DNIEmpleado, 
                              idPuestoEmpleado = @idPuestoEmpleado 
                          WHERE idEmpleado = @idEmpleado";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreEmpleado", empleado.nombreEmpleado);
                    command.Parameters.AddWithValue("@apellidoEmpleado", empleado.apellidoEmpleado);
                    command.Parameters.AddWithValue("@direccionEmpleado", empleado.direccionEmpleado);
                    command.Parameters.AddWithValue("@telefonoEmpleado", empleado.telefonoEmpleado);
                    command.Parameters.AddWithValue("@mailEmpleado", empleado.mailEmpleado);
                    command.Parameters.AddWithValue("@DNIEmpleado", empleado.DNIEmpleado);
                    command.Parameters.AddWithValue("@idPuestoEmpleado", empleado.idPuestoEmpleado);
                    command.Parameters.AddWithValue("@idEmpleado", empleado.idEmpleado);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteEmpleado(int id)
        {
            var query = "DELETE FROM Empleado WHERE idEmpleado = @idEmpleado";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idEmpleado", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
    }
}