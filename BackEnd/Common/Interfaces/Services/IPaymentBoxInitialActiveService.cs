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
    public interface IPaymentBoxInitialActiveService
    {
        AckEntity<PaymentBoxInitialActiveModel> Update(PaymentBoxInitialActiveModel model);
        Ack Delete(int id);
        AckEntity<PaymentBoxInitialActiveModel> Crear(PaymentBoxInitialActiveModel model);
        PaymentBoxInitialActiveModel Obtener(int id);

        List<PaymentBoxInitialActiveModel> GetAll();
    }
}
