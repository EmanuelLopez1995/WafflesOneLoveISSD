using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository.Repositories
{
    public class CajaRepository : ICajaRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public CajaRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<int> IniciarCaja(CajaModel caja)
        {

            var query = @"INSERT INTO Caja (activoInicial, importeInicial)
                              VALUES (@activoInicial, @importeInicial);
                              SELECT SCOPE_IDENTITY();"
            ;

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@activoInicial", caja.activoInicial);
                    command.Parameters.AddWithValue("@importeInicial", caja.importeInicial);

                    int idCaja = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idCaja;
                }
            }
        }
    }
}