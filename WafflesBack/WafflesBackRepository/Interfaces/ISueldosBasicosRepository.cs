using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ISueldosBasicosRepository
    {
        Task<int> UpdateSueldosBasicos(SueldosBasicosModel sueldosBasicos);
        Task<List<SueldosBasicosModel>> GetAllSueldosBasicos();
    }
}
