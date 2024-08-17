using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces; 

namespace WafflesBackRepository 
{
    public class RecetaRepository : IRecetaRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public RecetaRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<RecetaModel>> GetAllRecetas()
        {
            var recetasList = new List<RecetaModel>();
            var query = "SELECT * FROM Receta";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var receta = new RecetaModel
                            {
                                idReceta = reader.GetInt32(0),
                                nombreReceta = reader.GetString(1),
                                procedimiento = reader.GetString(2)
                            };
                            recetasList.Add(receta);
                        }
                    }
                }
            }
            return recetasList;
        }

        public async Task<int> AddReceta(RecetaModel receta)
        {
            var query = @"INSERT INTO Receta (nombreReceta, procedimiento)
                          OUTPUT INSERTED.IdReceta
                          VALUES (@NombreReceta, @Procedimiento)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreReceta", receta.nombreReceta);
                    command.Parameters.AddWithValue("@Procedimiento", receta.procedimiento);
                    int IdReceta = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return IdReceta;
                }
            }
        }

        public async Task<int> UpdateReceta(RecetaModel receta)
        {
            var query = @"UPDATE Receta
                          SET nombreReceta = @NombreReceta,
                              procedimiento = @Procedimiento
                          WHERE idReceta = @IdReceta";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreReceta", receta.nombreReceta);
                    command.Parameters.AddWithValue("@Procedimiento", receta.procedimiento);
                    command.Parameters.AddWithValue("@IdReceta", receta.idReceta);
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return (int)receta.idReceta;
                }
            }
        }

        public async Task<int> DeleteReceta(int idReceta)
        {
            var query = "DELETE FROM Receta WHERE idReceta = @IdReceta";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<RecetaModel> GetRecetaById(int idReceta)
        {
            var query = "SELECT * FROM Receta WHERE idReceta = @IdReceta";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new RecetaModel
                            {
                                idReceta = reader.GetInt32(0),
                                nombreReceta = reader.GetString(1),
                                procedimiento = reader.GetString(2)
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