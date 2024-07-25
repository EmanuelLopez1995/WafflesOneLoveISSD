using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> GetAllUsuarios();
        Task<int> AddUsuario(UsuarioModel usuario);
        Task<int> UpdateUsuario(UsuarioModel usuario);
        Task<int> DeleteUsuario(int id);
        Task<UsuarioModel> GetUsuarioPorId(int id);
    }
}