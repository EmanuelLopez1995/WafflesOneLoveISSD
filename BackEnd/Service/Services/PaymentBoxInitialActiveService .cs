using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;

namespace Service.Services
{
    public class PaymentBoxInitialActiveService : DataAccessAbstractService,IPaymentBoxInitialActiveService
    {
        public PaymentBoxInitialActiveService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AckEntity<PaymentBoxInitialActiveModel> Crear(PaymentBoxInitialActiveModel model)
        {
            var ack = new AckEntity<PaymentBoxInitialActiveModel>();
            //if (model.Email != "asdasdas")
            //{
            //    ack.Mensaje = "El Email No Es Valido";
            //    return ack;
            //}

            var paymentBoxInitialActive = new PaymentBoxInitialActive
            {
                InitialActive = model.InitialActive
               
            };

            UoW.PaymentBoxInitialActive.Add(paymentBoxInitialActive);
            UoW.Complete();

            model.Id = paymentBoxInitialActive.Id;

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }

        public Ack Delete(int id)
        {
            var ack = new Ack();

            var paymentBoxInitialActive = UoW.PaymentBoxInitialActive.Obtener(id);
            if (paymentBoxInitialActive == null)
            {
                ack.Mensaje = "El ID de la caja  No Existe";
                return ack;
            }

            UoW.PaymentBoxInitialActive.Remove(paymentBoxInitialActive);
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public PaymentBoxInitialActiveModel Obtener(int id)
        {
            var paymentBoxInitialActive = UoW.PaymentBoxInitialActive.Obtener(id);
            if (paymentBoxInitialActive == null)
                return null;

            return new PaymentBoxInitialActiveModel
            {
                InitialActive = (float?)paymentBoxInitialActive.InitialActive
            };
        }

        public List<PaymentBoxInitialActiveModel> GetAll()
        {
            var list = UoW.PaymentBoxInitialActive.GetAll();
            return list.Select(paymentBoxInitialActive => new PaymentBoxInitialActiveModel
            {
                InitialActive =(float?) paymentBoxInitialActive.InitialActive
               

            }).ToList();
        }

        public AckEntity<PaymentBoxInitialActiveModel> Update(PaymentBoxInitialActiveModel model)
        {
            var ack = new AckEntity<PaymentBoxInitialActiveModel>();

            var paymentBoxInitialActive = UoW.PaymentBoxInitialActive.Obtener(model.Id);
            if (paymentBoxInitialActive == null)
            {
                ack.Mensaje = "El id de caja a updatear no Existe";
                return ack;
            }

            paymentBoxInitialActive.InitialActive = model.InitialActive;
           



            UoW.Complete();

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }
    }
}