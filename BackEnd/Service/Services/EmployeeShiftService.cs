using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Model.query;
using Common.Repository;

namespace Service.Services
{
    public class EmployeeShiftService : DataAccessAbstractService, IEmployeeShiftService
    {
        public EmployeeShiftService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Ack Create(EmployeeShiftModel model)
        {
            var ack = new Ack();

            var employee = UoW.Employees.Obtener(model.EmployeeId);
            if (employee == null)
            {
                ack.Mensaje = "El empleado no existe";
                return ack;
            }

            employee.EmployeeShifts.Add(new EmployeeShift 
            { 
                StartDate = model.StartDate,
                EndDate = model.EndDate
            });

            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public Ack CloseShift(EmployeeShiftModel model)
        {
            var ack = new Ack();

            if (!model.EndDate.HasValue)
            {
                ack.Mensaje = "El campo EndDate es obligatorio.";
                return ack;
            }

            var employeeShift = UoW.EmployeeShifts.Obtener(new EmpoyeeShiftQueryModel 
            { 
                SinFinalizar = true,
                EmployeeId = model.EmployeeId
            });

            if (employeeShift == null)
            {
                ack.Mensaje = "El empleado no cuenta con ningún turno para cerrar";
                return ack;
            }

            employeeShift.EndDate = model.EndDate;
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }
    }
}