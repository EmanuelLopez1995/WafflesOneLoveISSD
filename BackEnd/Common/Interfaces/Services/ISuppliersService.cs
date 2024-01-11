using Common.Entities;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Service
{
    public interface ISuppliersService
    {
        AckEntity<SuppliersModel> Update(SuppliersModel model);
        Ack Delete(int id);
        AckEntity<SuppliersModel> Crear(SuppliersModel model);
        SuppliersModel Obtener(int id);

        List<SuppliersModel> GetAll();
    }
}
