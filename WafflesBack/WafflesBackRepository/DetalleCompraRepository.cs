using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class DetalleCompraRepository : IDetalleCompraRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public DetalleCompraRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<DetalleCompraModel>> GetDetallesByCompraId(int idCompra)
        {
            var detalleCompraList = new List<DetalleCompraModel>();
            var query = "SELECT * FROM DetalleCompra WHERE idCompra = @idCompra";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idCompra", idCompra);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var detalleCompra = new DetalleCompraModel
                            {
                                IdDetalleCompra = reader.GetInt32(0),
                                IdArticulo = reader.GetInt32(1),
                                Cantidad = reader.GetInt32(2),
                                PrecioUnitario = reader.GetDecimal(3),
                                Subtotal = reader.GetDecimal(4),
                                IdCompra = reader.GetInt32(5)
                            };
                            detalleCompraList.Add(detalleCompra);
                        }
                    }
                }
            }
            return detalleCompraList;
        }

        public async Task<int> AddDetalleCompra(DetalleCompraModel detalleCompra)
        {
            var query = @"INSERT INTO DetalleCompra (idArticulo, cantidad, precioUnitario, subtotal, idCompra)
                          VALUES (@idArticulo, @cantidad, @precioUnitario, @subtotal, @idCompra)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idArticulo", detalleCompra.IdArticulo);
                    command.Parameters.AddWithValue("@cantidad", detalleCompra.Cantidad);
                    command.Parameters.AddWithValue("@precioUnitario", detalleCompra.PrecioUnitario);
                    command.Parameters.AddWithValue("@subtotal", detalleCompra.Subtotal);
                    command.Parameters.AddWithValue("@idCompra", detalleCompra.IdCompra);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
    }
}