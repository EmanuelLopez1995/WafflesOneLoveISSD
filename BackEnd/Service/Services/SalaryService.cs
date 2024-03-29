using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;

namespace Service.Services
{
    public class SalaryService : DataAccessAbstractService, ISalaryService
    {
        public SalaryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AckEntity<SalaryModel> Crear(SalaryModel model)
        {
            var ack = new AckEntity<SalaryModel>();
            //if (model.Email != "asdasdas")
            //{
            //    ack.Mensaje = "El Email No Es Valido";
            //    return ack;
            //}

            var salary = new Salary
            {
                HoursWorked = model.HorasTrabajadas,
                HourType = model.TipoHora,
                SalaryTotal = model.SalarioTotal,
                HourValue = model.ValorHora
            };

            UoW.Salary.Add(salary);
            UoW.Complete();

            model.Id = salary.Id;

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }

        public Ack Delete(int id)
        {
            var ack = new Ack();

            var salary = UoW.Salary.Obtener(id);
            if (salary == null)
            {
                ack.Mensaje = "El salario no existe para el empleado mencionado";
                return ack;
            }

            UoW.Salary.Remove(salary);
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public SalaryModel Obtener(int id)
        {
            var Salary = UoW.Salary.Obtener(id);
            if (Salary == null)
                return null;

            return new SalaryModel
            {
                HorasTrabajadas = Salary.HoursWorked,
                TipoHora=Salary.HourType,
                SalarioTotal = Salary.SalaryTotal,
                ValorHora = Salary.HourValue

            };
        }

        public List<SalaryModel> GetAll()
        {
            var list = UoW.Salary.GetAll();
            return list.Select(salary => new SalaryModel
            {
                HorasTrabajadas = salary.HoursWorked,
                TipoHora = salary.HourType,
                SalarioTotal = salary.SalaryTotal,
                ValorHora = salary.HourValue

            }).ToList();
        }

        public AckEntity<SalaryModel> Update(SalaryModel model)
        {
            var ack = new AckEntity<SalaryModel>();

            var salary = UoW.Salary.Obtener(model.Id);
            if (salary == null)
            {
                ack.Mensaje = "El salario no existe para el empleado mencionado";
                return ack;
            }

            salary.SalaryTotal = salary.SalaryTotal;
            salary.HourValue= salary.HourValue;
            salary.HourType=salary.HourType;
            salary.HoursWorked = salary.HoursWorked;


            UoW.Complete();

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }
    }
}