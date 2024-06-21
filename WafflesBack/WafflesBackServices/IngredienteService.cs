﻿using System;
using System.Threading.Tasks;
using WafflesBackCommon.Models;
using WafflesBackRepository.Interfaces;
using WafflesBackRepository.Repositories;
using WafflesBackServices.Interfaces;

namespace WafflesBackServices
{
    public class IngredienteService : IIngredienteService

    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IArticuloPorIngredienteRepository _articuloPorIngredienteRepository;

        public IngredienteService(IIngredienteRepository ingredienteRepository, IArticuloPorIngredienteRepository articuloPorIngredienteRepository)
        {
            _ingredienteRepository = ingredienteRepository;
            _articuloPorIngredienteRepository = articuloPorIngredienteRepository;
        }

        public async Task<List<IngredienteModel>> GetAllIngredientes()
        {
            var listadoIngredientes = await _ingredienteRepository.GetAllIngredientes();

            foreach (var ingrediente in listadoIngredientes)
            {
                var articuloIds = await _articuloPorIngredienteRepository.GetArticulosPorIngredienteId((int)ingrediente.IdIngrediente);
                ingrediente.IdsArticulos = articuloIds;
            }

            return listadoIngredientes;
        }

        public async Task<int> AddIngrediente(IngredienteModel ingrediente)
        {
            try
            {
                int IdIngrediente = await _ingredienteRepository.AddIngrediente(ingrediente);

                foreach (var idArticulo in ingrediente.IdsArticulos)
                {
                    await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente(idArticulo, IdIngrediente);
                }

                return IdIngrediente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio al registrar ingrediente: {ex.Message}");
            }
        }

        public async Task<int> UpdateIngrediente(IngredienteModel ingrediente)
        {
            return await _ingredienteRepository.UpdateIngrediente(ingrediente);
        }

        public async Task<int> DeleteIngrediente(int id)
        {
            return await _ingredienteRepository.DeleteIngrediente(id);
        }

        public async Task<IngredienteModel> GetIngredienteById(int id)
        {


            return await _ingredienteRepository.GetIngredienteById(id);


        }

        // Métodos para manejar la relación ArticulosPorIngrediente
        // public async Task RegistrarArticuloPorIngrediente(ArticuloPorIngredienteModel model)
        // {
        //await _articuloPorIngredienteRepository.RegistrarArticulosPorIngrediente(model);
        //}

        //public async Task<List<ArticuloPorIngredienteModel>> ObtenerArticulosPorIngrediente(int idIngrediente)
        //{
        //    return await _articuloPorIngredienteRepository.ObtenerArticulosPorIngrediente(idIngrediente);
        //}

        //public async Task<bool> ActualizarArticuloPorIngrediente(ArticuloPorIngredienteModel model)
        //{
        //  return await _articuloPorIngredienteRepository.ActualizarArticuloPorIngrediente(model);
        //}
    }
}
 
