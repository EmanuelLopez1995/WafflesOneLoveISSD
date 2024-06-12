using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository
{
    public class SueldosBasicosRepository : ISueldosBasicosRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public SueldosBasicosRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<int> UpdateSueldosBasicos(SueldosBasicosModel sueldosBasicos)
        {
            string query = string.Empty;
            switch (sueldosBasicos.idSueldosBasicos)
            {
                case 1:
                    query = @"UPDATE SueldosBasicos 
                              SET valorHoraNormal = @valorHoraNormal, 
                                  valorHoraFerDom = @valorHoraFerDom
                              WHERE idSueldosBasicos = @idSueldosBasicos";
                    break;
                case 2:
                    query = @"UPDATE SueldosBasicos 
                              SET valorHoraNormal = @valorHoraNormal, 
                                  valorHoraFerDom = @valorHoraFerDom, 
                                  plusEncargado = @plusEncargado
                              WHERE idSueldosBasicos = @idSueldosBasicos";
                    break;
                case 3:
                    query = @"UPDATE SueldosBasicos 
                              SET basicoDueno = @basicoDueno
                              WHERE idSueldosBasicos = @idSueldosBasicos";
                    break;
                default:
                    // Si el ID no coincide con ningún caso, no se realiza ninguna actualización.
                    return 0;
            }

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valorHoraNormal", sueldosBasicos.valorHoraNormal ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@valorHoraFerDom", sueldosBasicos.valorHoraFerDom ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@plusEncargado", sueldosBasicos.plusEncargado ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@basicoDueno", sueldosBasicos.basicoDueno ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@idSueldosBasicos", sueldosBasicos.idSueldosBasicos);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
            }
        }
    }
}