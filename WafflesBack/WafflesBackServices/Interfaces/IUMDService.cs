using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface IUMDService
    {
        public Task<List<UMDModel>> GetAllUMDs();
        public Task<int> AddUMD(UMDModel umd);
        public Task<int> UpdateUMD(UMDModel umd);
        public Task<int> DeleteUMD(int id);
    }
}