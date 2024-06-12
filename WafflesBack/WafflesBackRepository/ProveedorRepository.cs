using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public ProveedorRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<ProveedorModel>> GetAllProveedores()
        {
            var proveedorList = new List<ProveedorModel>();
            var query = "SELECT * FROM Proveedor";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var proveedor = new ProveedorModel
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                RazonSocial = reader.GetString(2),
                                Direccion = reader.GetString(3),
                                Numero = reader.GetString(4),
                                Cuit = reader.GetString(5),
                                Email = reader.GetString(6),
                                Detalle = reader.GetString(7)
                            };
                            proveedorList.Add(proveedor);
                        }
                    }
                }
            }
            return proveedorList;
        }

        public async Task<int> AddProveedor(ProveedorModel proveedor)
        {
            var query = @"INSERT INTO Proveedor (Nombre, RazonSocial, Direccion, Numero, Cuit, Email, Detalle)
                  VALUES (@Nombre, @RazonSocial, @Direccion, @Numero, @Cuit, @Email, @Detalle)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                    command.Parameters.AddWithValue("@RazonSocial", proveedor.RazonSocial);
                    command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    command.Parameters.AddWithValue("@Numero", proveedor.Numero);
                    command.Parameters.AddWithValue("@Cuit", proveedor.Cuit);
                    command.Parameters.AddWithValue("@Email", proveedor.Email);
                    command.Parameters.AddWithValue("@Detalle", proveedor.Detalle);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> UpdateProveedor(ProveedorModel proveedor)
        {
            var query = @"UPDATE Proveedor 
                  SET Nombre = @Nombre, RazonSocial = @RazonSocial, 
                      Direccion = @Direccion, Numero = @Numero, 
                      Cuit = @Cuit, Email = @Email, Detalle = @Detalle 
                  WHERE Id = @Id";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                    command.Parameters.AddWithValue("@RazonSocial", proveedor.RazonSocial);
                    command.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    command.Parameters.AddWithValue("@Numero", proveedor.Numero);
                    command.Parameters.AddWithValue("@Cuit", proveedor.Cuit);
                    command.Parameters.AddWithValue("@Email", proveedor.Email);
                    command.Parameters.AddWithValue("@Detalle", proveedor.Detalle);
                    command.Parameters.AddWithValue("@Id", proveedor.Id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteProveedor(int id)
        {
            var query = "DELETE FROM Proveedor WHERE Id = @Id";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
    }
}