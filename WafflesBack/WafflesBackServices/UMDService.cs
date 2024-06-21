using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;
using Microsoft.Extensions.Configuration;
using WafflesBackCommon.Models;

namespace WafflesBackServices
{
    public class UMDService : IUMDService
    {
        private readonly IUMDRepository _umdRepository;

        public UMDService(IUMDRepository umdRepository, IConfiguration configuration)
        {
            _umdRepository = umdRepository;
        }

        public async Task<List<UMDModel>> GetAllUMDs()
        {
            try
            {
                return await _umdRepository.GetAllUMDs();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener todas las unidades de medida");
            }
        }

        public async Task<int> AddUMD(UMDModel umd)
        {
            try
            {
                return await _umdRepository.AddUMD(umd);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al insertar la unidad de medida");
            }
        }

        public async Task<int> UpdateUMD(UMDModel umd)
        {
            try
            {
                return await _umdRepository.UpdateUMD(umd);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar la unidad de medida");
            }
        }

        public async Task<int> DeleteUMD(int id)
        {
            try
            {
                return await _umdRepository.DeleteUMD(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al eliminar la unidad de medida");
            }
        }
    }
}