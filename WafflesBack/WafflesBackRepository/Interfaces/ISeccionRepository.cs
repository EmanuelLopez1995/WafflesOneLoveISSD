using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ISeccionRepository
    {
        Task<List<SeccionModel>> GetAllSecciones();
    }
}