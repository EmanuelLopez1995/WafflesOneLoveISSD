using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class SeccionRepository : ISeccionRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public SeccionRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<SeccionModel>> GetAllSecciones()
        {
            var seccionList = new List<SeccionModel>();
            var query = "SELECT * FROM Seccion";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var seccion = new SeccionModel
                            {
                                idSeccion = reader.GetInt32(0),
                                nombreSeccion = reader.GetString(1),
                            };
                            seccionList.Add(seccion);
                        }
                    }
                }
            }
            return seccionList;
        }
    }
}