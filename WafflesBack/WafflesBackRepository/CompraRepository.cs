using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public CompraRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<CompraModel>> GetAllCompras()
        {
            var compraList = new List<CompraModel>();
            var query = "SELECT * FROM Compra";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync())
                        {
                            var compra = new CompraModel
                            {
                                IdCompra = reader.GetInt32(0),
                                FechaCompra = reader.GetDateTime(1),
                                //Archivo = (byte[])reader[2],  // Assuming Archivo is VARBINARY
                                IdProveedor = reader.GetInt32(3),
                                Total = reader.GetDecimal(4),
                            };
                            compraList.Add(compra);
                        }
                    }
                }
            }
            return compraList;
        }

        public async Task<int> AddCompra(CompraModel compra)
        {
            var query = @"INSERT INTO Compra (fechaCompra, idProveedor, Total)
                  OUTPUT INSERTED.idCompra
                  VALUES (@fechaCompra, @idProveedor, @Total)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fechaCompra", compra.FechaCompra);
                    //command.Parameters.AddWithValue("@archivo", compra.Archivo);
                    command.Parameters.AddWithValue("@idProveedor", compra.IdProveedor);
                    command.Parameters.AddWithValue("@Total", compra.Total);

                    var id = (int)await command.ExecuteScalarAsync();
                    return id;
                }
            }
        }

        public async Task<int> UpdateCompra(CompraModel compra)
        {
            var query = @"UPDATE Compra 
                          SET fechaCompra = @fechaCompra, 
                              idProveedor = @idProveedor, Total = @Total 
                          WHERE idCompra = @idCompra";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fechaCompra", compra.FechaCompra);
                    command.Parameters.AddWithValue("@idProveedor", compra.IdProveedor);
                    command.Parameters.AddWithValue("@Total", compra.Total);
                    command.Parameters.AddWithValue("@idCompra", compra.IdCompra);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteCompra(int id)
        {
            var query = "DELETE FROM Compra WHERE idCompra = @idCompra";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCompra", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<CompraModel> GetCompraPorId(int id)
        {
            var query = "SELECT * FROM Compra WHERE idCompra = @idCompra";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCompra", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new CompraModel
                            {
                                IdCompra = reader.GetInt32(0),
                                FechaCompra = reader.GetDateTime(1),
                                Archivo = (byte[])reader[2],  // Assuming Archivo is VARBINARY
                                IdProveedor = reader.GetInt32(3),
                                Total = reader.GetDecimal(4),
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