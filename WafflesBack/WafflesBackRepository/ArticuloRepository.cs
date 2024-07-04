using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public ArticuloRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<ArticuloModel>> GetAllArticulo()
        {
            var articuloList = new List<ArticuloModel>();
            var query = "SELECT * FROM Articulo";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var articulo = new ArticuloModel
                            {
                                IdArticulo = reader.GetInt32(0),
                                nombreArticulo = reader.GetString(1),
                                marcaArticulo = reader.GetString(2),
                                stockMinimo = reader.GetDecimal(3),
                                stockActual = reader.GetDecimal(4),
                                esMateriaPrima = reader.GetBoolean(5),
                                pesoArticulo = reader.GetDecimal(6),
                                detalleArticulo = reader.GetString(7),
                                idUMD = reader.GetInt32(8),

                               
                            };
                            articuloList.Add(articulo);
                        }
                    }
                }
            }
            return articuloList;
        }

        public async Task<int> AddArticulo(ArticuloModel articulo)
        {
            var query = @"INSERT INTO Articulo (nombreArticulo,marcaArticulo,stockMinimo,stockActual,
                        esMateriaPrima,pesoArticulo,detalleArticulo,idUMD)
                        OUTPUT INSERTED.IdArticulo
                          VALUES (@nombreArticulo, @marcaArticulo, @stockMinimo, @stockActual, 
                          @esMateriaPrima, @pesoArticulo, @detalleArticulo,@idUMD)";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreArticulo", articulo.nombreArticulo);
                    command.Parameters.AddWithValue("@marcaArticulo", articulo.marcaArticulo);
                    command.Parameters.AddWithValue("@stockMinimo", articulo.stockMinimo);
                    command.Parameters.AddWithValue("@stockActual", articulo.stockActual);
                    command.Parameters.AddWithValue("@esMateriaPrima", articulo.esMateriaPrima);
                    command.Parameters.AddWithValue("@pesoArticulo", articulo.pesoArticulo);
                    command.Parameters.AddWithValue("@detalleArticulo", articulo.detalleArticulo);
                    command.Parameters.AddWithValue("@idUMD", articulo.idUMD);


                    int IdArticulo = Convert.ToInt32(await command.ExecuteScalarAsync());

                   
                    return IdArticulo;
                }
            }
        }

        public async Task<int> UpdateArticulo(ArticuloModel articulo)
        {
            var query = @"UPDATE Articulo 
                          SET nombreArticulo = @nombreArticulo, marcaArticulo = @marcaArticulo, 
                              stockMinimo = @stockMinimo, stockActual = @stockActual, 
                              esMateriaPrima = @esMateriaPrima, pesoArticulo = @pesoArticulo, 
                              detalleArticulo = @detalleArticulo, idUMD = @idUMD
                          WHERE IdArticulo = @IdArticulo";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreArticulo", articulo.nombreArticulo);
                    command.Parameters.AddWithValue("@marcaArticulo", articulo.marcaArticulo);
                    command.Parameters.AddWithValue("@stockMinimo", articulo.stockMinimo);
                    command.Parameters.AddWithValue("@stockActual", articulo.stockActual);
                    command.Parameters.AddWithValue("@esMateriaPrima", articulo.esMateriaPrima);
                    command.Parameters.AddWithValue("@pesoArticulo", articulo.pesoArticulo);
                    command.Parameters.AddWithValue("@detalleArticulo", articulo.detalleArticulo);
                    command.Parameters.AddWithValue("@idUMD", articulo.idUMD);
                    command.Parameters.AddWithValue("@IdArticulo", articulo.IdArticulo);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> UpdateArticuloEsIngrediente(int idArticulo)
        {
            var query = @"  UPDATE Articulo 
                            SET esMateriaPrima = 1
                            WHERE IdArticulo = @IdArticulo";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArticulo", idArticulo);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<int> DeleteArticulo(int id)
        {
            var query = "DELETE FROM Articulo WHERE IdArticulo = @IdArticulo";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArticulo", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }

        public async Task<ArticuloModel> GetArticuloPorId(int id)
        {
            var query = "SELECT * FROM Articulo WHERE IdArticulo = @IdArticulo";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdArticulo", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ArticuloModel
                            {
                                IdArticulo = reader.GetInt32(0),
                                nombreArticulo = reader.GetString(1),
                                marcaArticulo = reader.GetString(2),
                                stockMinimo = reader.GetDecimal(3),
                                stockActual = reader.GetDecimal(4),
                                esMateriaPrima = reader.GetBoolean(5),
                                pesoArticulo = reader.GetDecimal(6),
                                detalleArticulo = reader.GetString(7),
                                idUMD = reader.GetInt32(8)

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