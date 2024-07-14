using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IBilletesRepository
    {
        Task<List<BilleteModel>> GetAllBilletes();
        Task<int> AddBillete(BilleteModel billete);
        Task<int> UpdateBillete(BilleteModel billete);
        Task<int> DeleteBillete(int idBillete);
        Task<BilleteModel> GetBilleteById(int idBillete);
    }
}
