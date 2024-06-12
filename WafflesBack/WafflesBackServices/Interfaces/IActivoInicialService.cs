using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface IActivoInicialService
    {
        Task<ActivoInicialModel> GetActivoInicial(int id);
        Task<int> UpdateActivoInicial(ActivoInicialModel activoInicial);
    }
}