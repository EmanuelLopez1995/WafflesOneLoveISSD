using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class IngredientePorRecetaRepository : IIngredientePorRecetaRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public IngredientePorRecetaRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task RegistrarIngredientePorReceta(int idReceta, int idIngrediente, decimal cantidad)
        {
            var query = @"INSERT INTO IngredientePorReceta (IdReceta, IdIngrediente, cantidad)
                          VALUES (@IdReceta, @IdIngrediente, @cantidad);";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);
                    command.Parameters.AddWithValue("@cantidad", cantidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteIngredientePorReceta(int idReceta, int idIngrediente)
        {
            var query = @"DELETE FROM IngredientePorReceta WHERE IdReceta = @IdReceta AND IdIngrediente = @IdIngrediente;";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<IngredientePorRecetaModel>> GetAllIngredientesPorReceta(int idReceta)
        {
            var query = @"SELECT IdIngPorReceta, IdIngrediente, cantidad, IdReceta
                          FROM IngredientePorReceta
                          WHERE IdReceta = @IdReceta";

            var ingredientesPorReceta = new List<IngredientePorRecetaModel>();

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var model = new IngredientePorRecetaModel
                            {
                                IdIngPorReceta = reader.GetInt32(reader.GetOrdinal("IdIngPorReceta")),
                                IdIngrediente = reader.GetInt32(reader.GetOrdinal("IdIngrediente")),
                                Cantidad = reader.GetDecimal(reader.GetOrdinal("cantidad")),
                                IdReceta = reader.GetInt32(reader.GetOrdinal("IdReceta"))
                            };

                            ingredientesPorReceta.Add(model);
                        }
                    }
                }
            }
            return ingredientesPorReceta;
        }

        public async Task<bool> ActualizarIngredientePorReceta(IngredientePorRecetaModel model)
        {
            var query = @"UPDATE IngredientePorReceta 
                          SET IdIngrediente = @IdIngrediente, 
                              cantidad = @cantidad
                          WHERE IdReceta = @IdReceta";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngPorReceta", model.IdIngPorReceta);
                    command.Parameters.AddWithValue("@IdIngrediente", model.IdIngrediente);
                    command.Parameters.AddWithValue("@cantidad", model.Cantidad);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> ActualizarCantidadIngredientePorReceta(int idReceta, int idIngrediente, decimal cantidad)
        {
            var query = @"UPDATE IngredientePorReceta
                  SET Cantidad = @Cantidad
                  WHERE IdReceta = @IdReceta AND IdIngrediente = @IdIngrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);
                    command.Parameters.AddWithValue("@Cantidad", cantidad);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<int>> GetIngredientesPorRecetaId(int idReceta)
        {
            var query = @"SELECT IdIngrediente
                          FROM IngredientePorReceta
                          WHERE IdReceta = @IdReceta";

            var ingredientesIds = new List<int>();

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ingredientesIds.Add(reader.GetInt32(reader.GetOrdinal("IdIngrediente")));
                        }
                    }
                }
            }

            return ingredientesIds;
        }

        public async Task<int> GetRecetaPorIngredienteId(int idIngrediente)
        {
            var query = @"SELECT IdReceta
                          FROM IngredientePorReceta
                          WHERE IdIngrediente = @IdIngrediente";

            int idReceta = 0;

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            idReceta = reader.GetInt32(reader.GetOrdinal("IdReceta"));
                        }
                    }
                }
            }

            return idReceta;
        }
    }
}
