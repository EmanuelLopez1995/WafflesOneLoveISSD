using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;

namespace Service.Services
{
    public class PaymentBoxService : DataAccessAbstractService,IPaymentBoxService
    {
        public PaymentBoxService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AckEntity<PaymentBoxModel> Crear(PaymentBoxModel model)
        {
            var ack = new AckEntity<PaymentBoxModel>();
            //if (model.Email != "asdasdas")
            //{
            //    ack.Mensaje = "El Email No Es Valido";
            //    return ack;
            //}

            var paymentBox = new PaymentBox
            {
                InitialActive = model.InitialActive,
                CashWitdrawal = model.CashWitdrawal,
                InitialImport = model.InitialImport,
                FinalImport = model.FinalImport
            };

            UoW.PaymentBox.Add(paymentBox);
            UoW.Complete();

            model.Id = paymentBox.Id;

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }

        public Ack Delete(int id)
        {
            var ack = new Ack();

            var paymentBox = UoW.PaymentBox.Obtener(id);
            if (paymentBox == null)
            {
                ack.Mensaje = "El ID de la caja  No Existe";
                return ack;
            }

            UoW.PaymentBox.Remove(paymentBox);
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public PaymentBoxModel Obtener(int id)
        {
            var paymentBox = UoW.PaymentBox.Obtener(id);
            if (paymentBox == null)
                return null;

            return new PaymentBoxModel
            {
                InitialActive = paymentBox.InitialActive,
                CashWitdrawal = paymentBox.CashWitdrawal,
                InitialImport = paymentBox.InitialImport,
                FinalImport = paymentBox.FinalImport

            };
        }

        public List<PaymentBoxModel> GetAll()
        {
            var list = UoW.PaymentBox.GetAll();
            return list.Select(paymentBox => new PaymentBoxModel
            {
                InitialActive = paymentBox.InitialActive,
                CashWitdrawal = paymentBox.CashWitdrawal,
                InitialImport = paymentBox.InitialImport,
                FinalImport = paymentBox.FinalImport

            }).ToList();
        }

        public AckEntity<PaymentBoxModel> Update(PaymentBoxModel model)
        {
            var ack = new AckEntity<PaymentBoxModel>();

            var paymentBox = UoW.PaymentBox.Obtener(model.Id);
            if (paymentBox == null)
            {
                ack.Mensaje = "El id de caja a updatear no Existe";
                return ack;
            }

            paymentBox.InitialActive = model.InitialActive;
            paymentBox.InitialImport = model.InitialImport;
            paymentBox.CashWitdrawal = model.CashWitdrawal;
            paymentBox.FinalImport = model.FinalImport;



            UoW.Complete();

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }
    }
}