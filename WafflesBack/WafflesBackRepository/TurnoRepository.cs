using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Helpers;
using WafflesBackRepository.Interfaces;

namespace WafflesBackRepository.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly DataBaseConnection _connectionHelper;

        public TurnoRepository(DataBaseConnection connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<int> IniciarTurno(TurnoModel turno, int idCaja)
        {
            var query = @"INSERT INTO Turno (tipoTurno, fechaTurno, horaDelInicio, notasInicio, esFeriado, idCaja, idEncargadoTurno)
                          VALUES (@tipoTurno, @fechaTurno, @horaDelInicio, @notasInicio, @esFeriado, @idCaja, @idEncargadoTurno);
                          SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = _connectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tipoTurno", turno.tipoTurno);
                    command.Parameters.AddWithValue("@fechaTurno", turno.fechaTurno);
                    command.Parameters.AddWithValue("@horaDelInicio", turno.horaDelInicio);
                    command.Parameters.AddWithValue("@notasInicio", turno.notasInicio);
                    command.Parameters.AddWithValue("@esFeriado", turno.esFeriado);
                    command.Parameters.AddWithValue("@idCaja", idCaja);
                    command.Parameters.AddWithValue("@idEncargadoTurno", turno.idEncargadoTurno);

                    int idTurno = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return idTurno;
                }
            }
        }
    }
}