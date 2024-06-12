using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class SueldosBasicosService : ISueldosBasicosService
    {
        private readonly ISueldosBasicosRepository _sueldosBasicosRepository;

        public SueldosBasicosService(ISueldosBasicosRepository sueldosBasicosRepository)
        {
            _sueldosBasicosRepository = sueldosBasicosRepository;
        }

        public async Task<int> UpdateSueldosBasicos(SueldosBasicosModel sueldosBasicos)
        {
            try
            {
                return await _sueldosBasicosRepository.UpdateSueldosBasicos(sueldosBasicos);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar los sueldos básicos");
            }
        }
    }
}