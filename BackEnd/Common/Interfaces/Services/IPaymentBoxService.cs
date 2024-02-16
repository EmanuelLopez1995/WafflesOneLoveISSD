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
    public interface IPaymentBoxService
    {
        AckEntity<PaymentBoxModel> Update(PaymentBoxModel model);
        Ack Delete(int id);
        AckEntity<PaymentBoxModel> Crear(PaymentBoxModel model);
        PaymentBoxModel Obtener(int id);

        List<PaymentBoxModel> GetAll();
    }
}
