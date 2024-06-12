using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class ActivoInicialRepository : IActivoInicialRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public ActivoInicialRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<ActivoInicialModel> GetActivoInicial(int id)
        {
            var query = "SELECT * FROM ActivoInicial WHERE idActivoInicial = @idActivoInicial";
            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idActivoInicial", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var activoInicial = new ActivoInicialModel
                            {
                                IdActivoInicial = reader.GetInt32(0),
                                MontoActivoInicial = reader.GetDecimal(1)
                            };
                            return activoInicial;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<int> UpdateActivoInicial(ActivoInicialModel activoInicial)
        {
            var query = @"UPDATE ActivoInicial 
                          SET montoActivoInicial = @montoActivoInicial
                          WHERE idActivoInicial = @idActivoInicial";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@montoActivoInicial", activoInicial.MontoActivoInicial);
                    command.Parameters.AddWithValue("@idActivoInicial", activoInicial.IdActivoInicial);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
    }
}