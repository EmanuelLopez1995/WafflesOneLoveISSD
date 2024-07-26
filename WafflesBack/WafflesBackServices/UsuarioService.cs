using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackCommon.Models;

namespace WafflesBackServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioSeccionesRepository _usuarioSeccionesRepository;


        public UsuarioService(IUsuarioRepository usuarioRepository, IConfiguration configuration, IUsuarioSeccionesRepository usuarioSeccionesRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioSeccionesRepository = usuarioSeccionesRepository;
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            try
            {
                List<UsuarioModel> usuarios = await _usuarioRepository.GetAllUsuarios();

                usuarios.ForEach(async usuario => {
                    usuario.idsSecciones = await _usuarioSeccionesRepository.GetSeccionesPorUsuario((int)usuario.idUsuario);
                });

                return usuarios;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener todos los usuarios");
            }
        }

        public async Task<int> AddUsuario(UsuarioModel usuario)
        {
            try
            {
                int idUsuario =  await _usuarioRepository.AddUsuario(usuario);

                usuario.idsSecciones.ForEach(async idSeccion =>
                {
                    await _usuarioSeccionesRepository.AddUsuarioSeccion(idUsuario, idSeccion);
                });

                return idUsuario;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al insertar el usuario");
            }
        }

        public async Task<int> UpdateUsuario(UsuarioModel usuario)
        {
            try
            {
                return await _usuarioRepository.UpdateUsuario(usuario);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar el usuario");
            }
        }

        public async Task<int> DeleteUsuario(int id)
        {
            try
            {
                return await _usuarioRepository.DeleteUsuario(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al eliminar el usuario");
            }
        }

        public async Task<UsuarioModel> GetUsuarioPorId(int id)
        {
            try
            {
                return await _usuarioRepository.GetUsuarioPorId(id);
            }
            catch (Exception)
            {
                throw new ApplicationException($"Error al obtener el usuario con ID: {id}");
            }
        }
    }
}