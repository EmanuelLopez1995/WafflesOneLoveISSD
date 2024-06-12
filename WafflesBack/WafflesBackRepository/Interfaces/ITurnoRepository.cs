using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ITurnoRepository
    {
        Task<int> IniciarTurno(TurnoModel turno, int idCaja);
    }
}