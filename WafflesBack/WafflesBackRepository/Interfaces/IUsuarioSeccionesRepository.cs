using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IUsuarioSeccionesRepository
    {
        Task<int> AddUsuarioSeccion(int idUsuario, int idSeccion);
        Task<List<int>> GetSeccionesPorUsuario(int idUsuario);
        Task<int> DeleteUsuarioSeccionesPorUsuario(int idUsuario);
    }
}