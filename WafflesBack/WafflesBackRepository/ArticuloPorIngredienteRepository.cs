﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class ArticuloPorIngredienteRepository : IArticuloPorIngredienteRepository
    {
        private readonly DataBaseConnection _connectionHelper;


        public ArticuloPorIngredienteRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task RegistrarArticulosPorIngrediente(int idArticulo, int idIngrediente)
        {
            var query = @"INSERT INTO ArticulosPorIngrediente (IdIngrediente, IdArticulo)
                          VALUES (@IdIngrediente, @IdArticulo);";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);
                    command.Parameters.AddWithValue("@IdArticulo", idArticulo);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<ArticuloPorIngredienteModel>> ObtenerArticulosPorIngrediente(int idIngrediente)
        {
            var query = @"SELECT IdIngrediente, IdArticulo
                          FROM ArticulosPorIngrediente
                          WHERE IdIngrediente = @IdIngrediente"
            ;

            var articuloPorIngrediente = new List<ArticuloPorIngredienteModel>();

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var model = new ArticuloPorIngredienteModel
                            {
                                IdArtPorIngrediente = reader.GetInt32(reader.GetOrdinal("IdArtPorIngrediente")),
                                IdIngrediente = reader.GetInt32(reader.GetOrdinal("IdIngrediente")),
                                IdArticulo = reader.GetInt32(reader.GetOrdinal("IdArticulo"))
                            };

                            articuloPorIngrediente.Add(model);
                        }
                    }
                }
            }
            return articuloPorIngrediente;
        }

        public async Task<bool> ActualizarArticuloPorIngrediente(ArticuloPorIngredienteModel model)
        {
            var query = @"UPDATE ArticulosPorIngrediente 
                          SET IdIngrediente = @IdIngrediente, 
                              IdArticulo = @IdArticulo
                          WHERE IdArtPorIngrediente = @IdArtPorIngrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArtPorIngrediente", model.IdArtPorIngrediente);
                    command.Parameters.AddWithValue("@IdIngrediente", model.IdIngrediente);
                    command.Parameters.AddWithValue("@IdArticulo", model.IdArticulo);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<List<int>> GetArticulosPorIngredienteId(int idIngrediente)
        {
            var query = @"SELECT IdArticulo
                          FROM ArticulosPorIngrediente
                          WHERE IdIngrediente = @IdIngrediente";

            var articulosIds = new List<int>();

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngrediente", idIngrediente);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            articulosIds.Add(reader.GetInt32(reader.GetOrdinal("IdArticulo")));
                        }
                    }
                }
            }

            return articulosIds;
        }
    }
}
