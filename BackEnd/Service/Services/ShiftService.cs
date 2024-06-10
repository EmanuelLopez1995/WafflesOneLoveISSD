using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Model.Enum;
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

            var employee = UoW.Employees.Obtener(model.OpenEmployeeId);
            if (employee == null)
            {
                ack.Mensaje = "El empleado no existe";
                return ack;
            }

            UoW.Shifts.Add(new Shift
            {
                StartDate = model.StartDate.Date,
                TypeShift = (int)model.TypeShift,
                TypeShiftHoliday = (int?)model.TypeShiftHoliday,
                Notes = model.Notes,
                EndDate = model.EndDate?.Date,
                OpenByEmployeeId = employee.Id
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

            if (model.Id <= 0)
            {
                ack.Mensaje = "El campo Id es obligatorio.";
                return ack;
            }

            var Shift = UoW.Shifts.Obtener(new ShiftQueryModel
            {
                SinFinalizar = true,
                Id = model.Id,

            });

            if (Shift == null)
            {
                ack.Mensaje = "El encargado  no cuenta con ningún turno para cerrar";
                return ack;
            }
            Shift.ClosedByEmployeeId = model.ClosedEmployeeId;
            Shift.EndDate = model.EndDate;
            try
            {
                UoW.Complete();
                ack.Exito = true;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el guardado
                ack.Mensaje = $"Ocurrió un error al cerrar el turno: {ex.Message}";
                ack.Exito = false;
            }

            return ack;
        }

        public AckEntity<ShiftModel> GetShift(ShiftModel model)
        {
            var ack = new AckEntity<ShiftModel>();
            if (model == null || model.Id <= 0)
            {
                ack.Mensaje = "El shift no existe";
                return ack;
            }

            var shift = UoW.Shifts.Obtener(model.Id);
            if (shift == null)
            {
                ack.Mensaje = "El shift no existe";
                return ack;
            }

            var employeeShifts = UoW.EmployeeShifts.ConsultarListado(new EmpoyeeShiftQueryModel { ShiftId = shift.Id });

            var response = new ShiftModel
            {
                ClosedEmployeeId = shift.ClosedByEmployeeId,
                OpenEmployeeId = shift.OpenByEmployeeId,
                EndDate = shift.EndDate,
                Id = shift.Id,
                Notes = shift.Notes,
                StartDate = shift.StartDate,
                TypeShift = (TypeShiftEnum)shift.TypeShift,
                TypeShiftHoliday = (TypeShiftHolidayEnum?)shift.TypeShiftHoliday,
                EmployeeShifts = employeeShifts.Select(x => new EmployeeShiftModel
                {
                    cashier = x.cashier,
                    EmployeeId = x.EmployeeId,
                    EndDate = x.EndDate,
                    EndTimeHours = x.EndTimeHours,
                    EndTimeMinutes = x.EndTimeMinutes,
                    NotesAdmission = x.NotesAdmission,
                    NotesEnd = x.NotesEnd,
                    StartDate = x.StartDate,
                    StartTimeHours = x.StartTimeHours,
                    StartTimeMinutes = x.StartTimeMinutes,
                    ShiftId=x.ShiftId
                })
            };

            ack.Entity = response;
            ack.Exito = true;
            return ack;
        }
    }
}