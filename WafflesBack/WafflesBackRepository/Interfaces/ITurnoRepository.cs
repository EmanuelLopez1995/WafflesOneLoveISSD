using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ITurnoRepository
    {
        Task<int> IniciarTurno(TurnoModel turno, int idCaja);
        Task<TurnoModel> ObtenerTurnoEnCurso();
        Task<bool> ActualizarTurnoEnCurso(TurnoModel turno);
        Task<bool> FinalizarTurnoEnCurso(TurnoModel turno);
    }
}