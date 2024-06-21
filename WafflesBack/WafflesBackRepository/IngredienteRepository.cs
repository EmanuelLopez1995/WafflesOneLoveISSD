using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class IngredienteRepository : IIngredienteRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public IngredienteRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<IngredienteModel>> GetAllIngredientes()
        {
            var ingredientesList = new List<IngredienteModel>();
            var query = "SELECT * FROM Ingrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var ingrediente = new IngredienteModel
                            {
                                IdIngrediente = reader.GetInt32(0),
                                nombreIngrediente = reader.GetString(1),
                                stockMinimo = reader.GetDecimal(2),
                                stockActual = reader.GetDecimal(3),
                                detalleIngrediente = reader.GetString(4),
                                idUMD = reader.GetInt32(5)
                            };
                            ingredientesList.Add(ingrediente);
                        }
                    }
                }
            }
            return ingredientesList;
        }

        public async Task<int> AddIngrediente(IngredienteModel ingrediente)
        {
            var query = @"
                    INSERT INTO Ingrediente (nombreIngrediente, stockMinimo, stockActual, detalleIngrediente, idUMD)
                    OUTPUT INSERTED.idIngrediente
                    VALUES (@nombreIngrediente, @stockMinimo, @stockActual, @detalleIngrediente, @idUMD)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreIngrediente", ingrediente.nombreIngrediente);
                    command.Parameters.AddWithValue("@stockMinimo", ingrediente.stockMinimo);
                    command.Parameters.AddWithValue("@stockActual", ingrediente.stockActual);
                    command.Parameters.AddWithValue("@detalleIngrediente", ingrediente.detalleIngrediente);
                    command.Parameters.AddWithValue("@idUMD", ingrediente.idUMD);

                    int idIngrediente = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idIngrediente;
                }
            }
        }

        public async Task<int> UpdateIngrediente(IngredienteModel ingrediente)
        {
            var query = @"UPDATE Ingrediente 
                          SET nombreIngrediente = @nombreIngrediente, stockMinimo = @stockMinimo, 
                              stockActual = @stockActual, detalleIngrediente = @detalleIngrediente, 
                              idUMD = @idUMD
                          WHERE IdIngrediente = @IdIngrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreIngrediente", ingrediente.nombreIngrediente);
                    command.Parameters.AddWithValue("@stockMinimo", ingrediente.stockMinimo);
                    command.Parameters.AddWithValue("@stockActual", ingrediente.stockActual);
                    command.Parameters.AddWithValue("@detalleIngrediente", ingrediente.detalleIngrediente);
                    command.Parameters.AddWithValue("@idUMD", ingrediente.idUMD);
                    command.Parameters.AddWithValue("@IdIngrediente", ingrediente.IdIngrediente);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteIngrediente(int id)
        {
            var query = "DELETE FROM Ingrediente WHERE IdIngrediente = @IdIngrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngrediente", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<IngredienteModel> GetIngredienteById(int id)
        {
            var query = "SELECT * FROM Ingrediente WHERE IdIngrediente = @IdIngrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdIngrediente", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new IngredienteModel
                            {
                                IdIngrediente = reader.GetInt32(0),
                                nombreIngrediente = reader.GetString(1),
                                stockMinimo = reader.GetDecimal(2),
                                stockActual = reader.GetDecimal(3),
                                detalleIngrediente = reader.GetString(4),
                                idUMD = reader.GetInt32(5)
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
