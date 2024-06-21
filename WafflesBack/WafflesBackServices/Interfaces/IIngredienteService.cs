using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface IIngredienteService
    {
        public Task<List<IngredienteModel>> GetAllIngredientes();
        public Task<int> AddIngrediente(IngredienteModel ingrediente);
        public Task<int> UpdateIngrediente(IngredienteModel ingrediente);
        public Task<int> DeleteIngrediente(int id);
        public Task<IngredienteModel> GetIngredienteById(int id);
    }
}