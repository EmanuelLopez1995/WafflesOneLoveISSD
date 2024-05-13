using Common.Entities;
using Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PaymentBoxInitialActiveRepository : Repository<PaymentBoxInitialActive>, IPaymentBoxInitialActiveRepository
    {
        public PaymentBoxInitialActiveRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public PaymentBoxInitialActive obtener(int id)
        {


            return context.PaymentBoxInitialActive.FirstOrDefault(x => x.Id == id);
        }
    }
}
 