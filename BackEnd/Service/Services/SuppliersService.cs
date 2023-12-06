using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;

namespace Service.Services
{
    public class SuppliersService : DataAccessAbstractService,ISuppliersService
    {
        public SuppliersService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AckEntity<SuppliersModel> Crear(SuppliersModel model)
        {
            var ack = new AckEntity<SuppliersModel>();
            //if (model.Email != "asdasdas")
            //{
            //    ack.Mensaje = "El Email No Es Valido";
            //    return ack;
            //}

            var suppliers = new Suppliers
            {
               Name=model.Nombre,
               SocialReason=model.RazonSocial,
               Addrees=model.Direccion,
               PhoneNumber= model.Numero,
               Cuit=model.Cuit, 
               Email=model.Email,
            };

            UoW.Suppliers.Add(suppliers);
            UoW.Complete();

            model.Id = suppliers.Id;

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }

        public Ack Delete(int id)
        {
            var ack = new Ack();

            var suppliers = UoW.Suppliers.Obtener(id);
            if (suppliers == null)
            {
                ack.Mensaje = "El proveedor No Existe";
                return ack;
            }

            UoW.Suppliers.Remove(suppliers);
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public SuppliersModel Obtener(int id)
        {
            var suppliers = UoW.Suppliers.Obtener(id);
            if (suppliers == null)
                return null;

            return new SuppliersModel
            {
                Nombre = suppliers.Name,
                RazonSocial=suppliers.SocialReason,
                Direccion=suppliers.Addrees,
                Numero=suppliers.PhoneNumber,
                Cuit=suppliers.Cuit,
                Email=suppliers.Email

            };
        }

        public AckEntity<SuppliersModel> Update(SuppliersModel model)
        {
            var ack = new AckEntity<SuppliersModel>();

            var suppliers = UoW.Suppliers.Obtener(model.Id);
            if (suppliers == null)
            {
                ack.Mensaje = "El proveedor No Existe";
                return ack;
            }
            suppliers.Name = model.Nombre;
            suppliers.SocialReason = model.RazonSocial;
            suppliers.Addrees=model.Direccion;
            suppliers.PhoneNumber = model.Numero;
            suppliers.Cuit = model.Cuit;
            suppliers.Email= model.Email;

            UoW.Complete();

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }
    }
}