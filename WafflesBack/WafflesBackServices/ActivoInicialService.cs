using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class ActivoInicialService : IActivoInicialService
    {
        private readonly IActivoInicialRepository _activoInicialRepository;

        public ActivoInicialService(IActivoInicialRepository activoInicialRepository)
        {
            _activoInicialRepository = activoInicialRepository;
        }

        public async Task<ActivoInicialModel> GetActivoInicial(int id)
        {
            try
            {
                return await _activoInicialRepository.GetActivoInicial(id);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al obtener el activo inicial");
            }
        }

        public async Task<int> UpdateActivoInicial(ActivoInicialModel activoInicial)
        {
            try
            {
                return await _activoInicialRepository.UpdateActivoInicial(activoInicial);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al actualizar el activo inicial");
            }
        }
    }
}