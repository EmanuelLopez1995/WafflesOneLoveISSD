using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
                          SELECT SCOPE_IDENTITY();";

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

        public async Task<CajaModel> GetCajaPorId(int idCaja)
        {
            var query = @"SELECT idCaja, activoInicial, retiroCaja, importeInicial, importeFinal
                          FROM Caja
                          WHERE idCaja = @idCaja";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCaja", idCaja);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new CajaModel
                            {
                                idCaja = reader.GetInt32(reader.GetOrdinal("idCaja")),
                                activoInicial = reader.GetDecimal(reader.GetOrdinal("activoInicial")),
                                retiroCaja = reader.IsDBNull(reader.GetOrdinal("retiroCaja")) ? 0 : reader.GetDecimal(reader.GetOrdinal("retiroCaja")),
                                importeInicial = reader.GetDecimal(reader.GetOrdinal("importeInicial")),
                                importeFinal = reader.IsDBNull(reader.GetOrdinal("importeFinal")) ? 0 : reader.GetDecimal(reader.GetOrdinal("importeFinal"))
                            };
                        }
                        else
                        {
                            return null; // Devuelve null si no se encuentra la caja
                        }
                    }
                }
            }
        }
    }
}