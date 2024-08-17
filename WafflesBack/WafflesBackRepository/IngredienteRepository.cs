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
                                detalleIngrediente = reader.GetString(2)
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
                    INSERT INTO Ingrediente (nombreIngrediente,detalleIngrediente)
                    OUTPUT INSERTED.idIngrediente
                    VALUES (@nombreIngrediente,@detalleIngrediente)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreIngrediente", ingrediente.nombreIngrediente);
                   
                    command.Parameters.AddWithValue("@detalleIngrediente", ingrediente.detalleIngrediente);
                    

                    int idIngrediente = Convert.ToInt32(await command.ExecuteScalarAsync());


                    return idIngrediente;
                }
            }
        }

        public async Task<int> UpdateIngrediente(IngredienteModel ingrediente)
        {
            var query = @"UPDATE Ingrediente 
                          SET nombreIngrediente = @nombreIngrediente,detalleIngrediente = @detalleIngrediente    
                          WHERE IdIngrediente = @IdIngrediente";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreIngrediente", ingrediente.nombreIngrediente);
                    
                    command.Parameters.AddWithValue("@detalleIngrediente", ingrediente.detalleIngrediente);
                    
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
                                detalleIngrediente = reader.GetString(2)
                              
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
