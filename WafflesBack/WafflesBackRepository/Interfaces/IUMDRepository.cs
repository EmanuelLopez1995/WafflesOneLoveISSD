using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IUMDRepository
    {
        Task<List<UMDModel>> GetAllUMDs();
        Task<int> AddUMD(UMDModel umd);
        Task<int> UpdateUMD(UMDModel umd);
        Task<int> DeleteUMD(int id);
    }
}