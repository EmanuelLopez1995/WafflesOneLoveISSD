using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public UsuarioRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            var usuarioList = new List<UsuarioModel>();
            var query = "SELECT * FROM Usuario";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new UsuarioModel
                            {
                                idUsuario = reader.GetInt32(0),
                                nombreUsuario = reader.GetString(1),
                                claveUsuario = reader.GetString(2)
                            };
                            usuarioList.Add(usuario);
                        }
                    }
                }
            }
            return usuarioList;
        }

        public async Task<int> AddUsuario(UsuarioModel usuario)
        {
            var query = @"INSERT INTO Usuario (nombreUsuario, claveUsuario)
                          OUTPUT INSERTED.idUsuario
                          VALUES (@nombreUsuario, @claveUsuario)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
                    command.Parameters.AddWithValue("@claveUsuario", usuario.claveUsuario);

                    var id = (int)await command.ExecuteScalarAsync();
                    return id;
                }
            }
        }

        public async Task<int> UpdateUsuario(UsuarioModel usuario)
        {
            var query = @"UPDATE Usuario 
                          SET nombreUsuario = @nombreUsuario, 
                              claveUsuario = @claveUsuario 
                          WHERE idUsuario = @idUsuario";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
                    command.Parameters.AddWithValue("@claveUsuario", usuario.claveUsuario);
                    command.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteUsuario(int id)
        {
            var query = "DELETE FROM Usuario WHERE idUsuario = @idUsuario";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<UsuarioModel> GetUsuarioPorId(int id)
        {
            var query = "SELECT * FROM Usuario WHERE idUsuario = @idUsuario";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new UsuarioModel
                            {
                                idUsuario = reader.GetInt32(0),
                                nombreUsuario = reader.GetString(1),
                                claveUsuario = reader.GetString(2)
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