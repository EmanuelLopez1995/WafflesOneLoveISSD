using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface ISeccionService
    {
        Task<List<SeccionModel>> GetAllSecciones();
    }
}