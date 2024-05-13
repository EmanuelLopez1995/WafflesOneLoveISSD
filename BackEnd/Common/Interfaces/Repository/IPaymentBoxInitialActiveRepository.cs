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
    public interface IPaymentBoxInitialActiveRepository : IRepository<PaymentBoxInitialActive>
    {
        PaymentBoxInitialActive Obtener(int id);
    }
}
