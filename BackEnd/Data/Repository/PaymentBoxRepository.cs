using Common.Entities;
using Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PaymentBoxRepository : Repository<PaymentBox>, IPaymentBoxRepository
    {
        public PaymentBoxRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public PaymentBox obtener(int id)
        {


            return context.PaymentBox.FirstOrDefault(x => x.Id == id);
        }
    }
}
 