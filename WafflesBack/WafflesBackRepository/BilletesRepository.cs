using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class BilletesRepository : IBilletesRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public BilletesRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<BilleteModel>> GetAllBilletes()
        {
            var billetesList = new List<BilleteModel>();
            var query = "SELECT * FROM Billetes";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var billete = new BilleteModel
                            {
                                IdBillete = reader.GetInt32(0),
                                ValorBillete = reader.GetInt32(1)
                            };
                            billetesList.Add(billete);
                        }
                    }
                }
            }
            return billetesList;
        }

        public async Task<int> AddBillete(BilleteModel billete)
        {
            var query = @"INSERT INTO Billetes (ValorBillete)
                          OUTPUT INSERTED.IdBillete
                          VALUES (@ValorBillete)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ValorBillete", billete.ValorBillete);
                    int IdBillete = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return IdBillete;
                }
            }
        }

        public async Task<int> UpdateBillete(BilleteModel billete)
        {
            var query = @"UPDATE Billetes
                          SET ValorBillete = @ValorBillete
                          WHERE IdBillete = @IdBillete";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ValorBillete", billete.ValorBillete);
                    command.Parameters.AddWithValue("@IdBillete", billete.IdBillete);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteBillete(int idBillete)
        {
            var query = "DELETE FROM Billetes WHERE IdBillete = @IdBillete";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdBillete", idBillete);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<BilleteModel> GetBilleteById(int idBillete)
        {
            var query = "SELECT * FROM Billetes WHERE IdBillete = @IdBillete";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdBillete", idBillete);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new BilleteModel
                            {
                                IdBillete = reader.GetInt32(0),
                                ValorBillete = reader.GetInt32(1)
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
