using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackServices.Interfaces
{
    public interface IArticuloService
    {
        public Task<List<ArticuloModel>> GetAllArticulo();
        public Task<int> AddArticulo(ArticuloModel articulo);
        public Task<int> UpdateArticulo(ArticuloModel articulo);
        public Task<int> DeleteArticulo(int id);
        public Task<ArticuloModel> GetArticuloPorId(int id);
    }
}
