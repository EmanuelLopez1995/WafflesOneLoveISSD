using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface ISueldosBasicosService
    {
        Task<int> UpdateSueldosBasicos(SueldosBasicosModel sueldosBasicos);
        Task<List<SueldosBasicosModel>> GetAllSueldosBasicos();
    }
}