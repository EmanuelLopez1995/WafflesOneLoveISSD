using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ICajaRepository
    {
        Task<int> IniciarCaja(CajaModel caja);
        Task<CajaModel> GetCajaPorId(int idCaja);
        Task<bool> ActualizarCajaEnCurso(CajaModel caja, int idCaja);
        Task<bool> FinalizarCajaEnCurso(CajaModel caja, int idCaja);
    }
}