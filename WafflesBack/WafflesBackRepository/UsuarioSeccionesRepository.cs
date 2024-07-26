using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class UsuarioSeccionesRepository : IUsuarioSeccionesRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public UsuarioSeccionesRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<int> AddUsuarioSeccion(int idUsuario, int idSeccion)
         {
            var query = @"INSERT INTO UsuarioSecciones (idUsuario, idSeccion)
                          VALUES (@idUsuario, @idSeccion)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@idSeccion", idSeccion);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<List<int>> GetSeccionesPorUsuario(int idUsuario)
        {
            var seccionesList = new List<int>();
            var query = "SELECT idSeccion FROM UsuarioSecciones WHERE idUsuario = @idUsuario";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            seccionesList.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return seccionesList;
        }
    }
}