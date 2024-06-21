using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class UMDRepository : IUMDRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public UMDRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<UMDModel>> GetAllUMDs()
        {
            var umdList = new List<UMDModel>();
            var query = "SELECT * FROM UMD";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var umd = new UMDModel
                            {
                                idUMD = reader.GetInt32(0),
                                nombreUMD = reader.GetString(1),
                                nombreCortoUMD = reader.GetString(2)
                            };
                            umdList.Add(umd);
                        }
                    }
                }
            }
            return umdList;
        }

        public async Task<int> AddUMD(UMDModel umd)
        {
            var query = @"INSERT INTO UMD (nombreUMD, nombreCortoUMD)
                          VALUES (@nombreUMD, @nombreCortoUMD)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUMD", umd.nombreUMD);
                    command.Parameters.AddWithValue("@nombreCortoUMD", umd.nombreCortoUMD);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> UpdateUMD(UMDModel umd)
        {
            var query = @"UPDATE UMD 
                          SET nombreUMD = @nombreUMD, nombreCortoUMD = @nombreCortoUMD
                          WHERE idUMD = @idUMD";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUMD", umd.nombreUMD);
                    command.Parameters.AddWithValue("@nombreCortoUMD", umd.nombreCortoUMD);
                    command.Parameters.AddWithValue("@idUMD", umd.idUMD);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteUMD(int id)
        {
            var query = "DELETE FROM UMD WHERE idUMD = @idUMD";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUMD", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
    }
}