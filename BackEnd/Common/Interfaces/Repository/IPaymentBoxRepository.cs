using Common.Entities;
using Common.Model.query;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Repository
{
    public interface IPaymentBoxRepository : IRepository<PaymentBox>
    {
        PaymentBox Obtener(int id);
    }
}
