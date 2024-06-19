using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface ITurnoService
    {
        Task<int> IniciarTurno(TurnoModel turno);
        Task<TurnoModel> GetTurnoEnCurso();
        Task<bool> UpdateTurnoEnCurso(TurnoModel turno);
    }
}