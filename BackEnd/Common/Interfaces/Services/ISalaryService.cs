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
    public interface ISalaryService
    {
        AckEntity<SalaryModel> Update(SalaryModel model);
        Ack Delete(int id);
        AckEntity<SalaryModel> Crear(SalaryModel model);
        SalaryModel Obtener(int id);

        List<SalaryModel> GetAll();
    }
}
