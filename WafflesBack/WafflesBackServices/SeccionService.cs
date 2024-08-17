using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackCommon.Models;

namespace WafflesBackServices
{
    public class SeccionService : ISeccionService
    {
        private readonly ISeccionRepository _seccionRepository;


        public SeccionService(ISeccionRepository seccionRepository, IConfiguration configuration)
        {
            _seccionRepository = seccionRepository;
        }

        public async Task<List<SeccionModel>> GetAllSecciones()
        {
            try
            {
                List<SeccionModel> secciones = await _seccionRepository.GetAllSecciones();

                return secciones;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener todas las secciones");
            }
        }
    }
}