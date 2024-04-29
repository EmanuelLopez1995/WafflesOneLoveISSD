using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Model.query;
using Common.Repository;

namespace Service.Services
{
    public class ShiftService : DataAccessAbstractService, IShiftService
    {
        public ShiftService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Ack Create(ShiftModel model)
        {
            var ack = new Ack();

            var employee = UoW.Employees.Obtener(model.EmployeeId);
            if (employee == null)
            {
                ack.Mensaje = "El empleado no existe";
                return ack;
            }


            employee.Shifts.Add(new Shift
            {
                StartDate = model.StartDate.Date,
                TypeShift = (int)model.TypeShift,
                TypeShiftHoliday = (int)model.TypeShiftHoliday,
                Notes = model.Notes,
                EndDate = model.EndDate?.Date
            });


            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public Ack CloseShift(ShiftModel model)
        {
            var ack = new Ack();

            if (!model.EndDate.HasValue)
            {
                ack.Mensaje = "El campo EndDate es obligatorio.";
                return ack;
            }


            var Shift = UoW.Shifts.Obtener(new ShiftQueryModel
            {
                SinFinalizar = true,
                EmployeeId = model.EmployeeId
            });

            if (Shift == null)
            {
                ack.Mensaje = "El encargado  no cuenta con ningún turno para cerrar";
                return ack;
            }

            Shift.EndDate = model.EndDate;
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }
    }
}