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

            var isShiftOpen = UoW.EmployeeShifts.IsShiftOpen(model.EmployeeId, model.StartDate.Date);
            if (!isShiftOpen)
            {
                ack.Mensaje = "El turno ya está abierto";
                return ack;
            }

            employee.EmployeeShifts.Add(new EmployeeShift
            {
                StartDate = model.StartDate.Date,
                StartTimeHours=model.StartTimeHours,
                StartTimeMinutes=model.StartTimeMinutes,
                EndTimeHours=model.EndTimeHours,
                EndTimeMinutes=model.EndTimeMinutes,
                NotesAdmission = model.NotesAdmission,
                NotesEnd = model.NotesEnd,
                cashier =model.cashier,
                EndDate = model.EndDate?.Date,
                ShiftId=model.ShiftId

            }); ;


                UoW.Complete();
                ack.Exito = true;
            
           
            
               // ack.Mensaje = $"Ocurrió un error al agregar el turno del empleado ";
                //ack.Exito = false;
            
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
            if (!model.EndTimeHours.HasValue && !model.EndTimeMinutes.HasValue)
            {
                ack.Mensaje = "El campo EndTimeHours y EndTimeMinutes es obligatorio.";
                return ack;
            }

            var employeeShift = UoW.EmployeeShifts.Obtener(new EmpoyeeShiftQueryModel
            {
                SinFinalizar = true,
                SinFinalizarHora = true,
                EmployeeId = model.EmployeeId
            });

            if (employeeShift == null)
            {
                ack.Mensaje = "El empleado no cuenta con ningún turno para cerrar";
                return ack;
            }

            employeeShift.EndDate = model.EndDate?.Date;
            employeeShift.EndTimeHours = model.EndTimeHours;
            employeeShift.EndTimeMinutes = model.EndTimeMinutes;
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }
    }
}