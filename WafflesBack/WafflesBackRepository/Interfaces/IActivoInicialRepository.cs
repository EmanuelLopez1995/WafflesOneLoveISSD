using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IActivoInicialRepository
    {
        Task<ActivoInicialModel> GetActivoInicial(int id);
        Task<int> UpdateActivoInicial(ActivoInicialModel activoInicial);
    }
}