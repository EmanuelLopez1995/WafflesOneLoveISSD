using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class BilletesService : IBilletesService
    {
        private readonly IBilletesRepository _billetesRepository;

        public BilletesService(IBilletesRepository billetesRepository)
        {
            _billetesRepository = billetesRepository;
        }

        public async Task<List<BilleteModel>> GetAllBilletes()
        {
            return await _billetesRepository.GetAllBilletes();
        }

        public async Task<int> AddBillete(BilleteModel billete)
        {
            return await _billetesRepository.AddBillete(billete);
        }

        public async Task<int> UpdateBillete(BilleteModel billete)
        {
            return await _billetesRepository.UpdateBillete(billete);
        }

        public async Task<int> DeleteBillete(int idBillete)
        {
            return await _billetesRepository.DeleteBillete(idBillete);
        }

        public async Task<BilleteModel> GetBilleteById(int idBillete)
        {
            return await _billetesRepository.GetBilleteById(idBillete);
        }
    }
}