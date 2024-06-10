using Common.Entities;
using Common.Interfaces.Repository;
using Common.Model.query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EmployeeShiftRepository : Repository<EmployeeShift>, IEmployeeShiftRepository
    {
        public EmployeeShiftRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public EmployeeShift Obtener(EmpoyeeShiftQueryModel queryModel)
        {
            return ObtenerQueryable(queryModel).FirstOrDefault();
        }

        public IEnumerable<EmployeeShift> ConsultarListado(EmpoyeeShiftQueryModel queryModel)
        {
            return ObtenerQueryable(queryModel).ToList();
        }

        private IQueryable<EmployeeShift> ObtenerQueryable(EmpoyeeShiftQueryModel queryModel)
        {
            var query = context.EmployeeShifts.AsQueryable();

            if (queryModel.ShiftId.HasValue) query = query.Where(x => x.ShiftId == queryModel.ShiftId.Value);
            if (queryModel.EmployeeId.HasValue) query = query.Where(x => x.EmployeeId == queryModel.EmployeeId);
            if (queryModel.SinFinalizar.HasValue)
            {
                if (queryModel.SinFinalizar.Value)
                {
                    query = query.Where(x => !x.EndDate.HasValue);
                }
                else
                {
                    query = query.Where(x => x.EndDate.HasValue);
                }
            }


            if (queryModel.SinFinalizarHora.HasValue)
            {
                if (queryModel.SinFinalizarHora.Value)
                {
                    query = query.Where(x => !x.EndTimeHours.HasValue);
                }
                else
                {
                    query = query.Where(x => x.EndTimeHours.HasValue);
                }
            }
            if (queryModel.SinFinalizarHora.HasValue)
            {
                if (queryModel.SinFinalizarHora.Value)
                {
                    query = query.Where(x => !x.EndTimeMinutes.HasValue);
                }
                else
                {
                    query = query.Where(x => x.EndTimeMinutes.HasValue);
                }
            }
            return query;
        }
        public bool IsShiftOpen(int employeeId, DateTime startDate)
        {
            return !context.EmployeeShifts
                .Any(es => es.EmployeeId == employeeId &&
                           es.StartDate == startDate &&
                           es.EndDate == null);
        }
    }
}
