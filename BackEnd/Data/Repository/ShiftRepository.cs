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
    public class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        public ShiftRepository(Context context) : base(context)
        {
        }
        public Context context => Context as Context;

        public Shift Obtener(ShiftQueryModel queryModel)
        {
            return ObtenerQueryable(queryModel).FirstOrDefault();
        }

        public IEnumerable<Shift> ConsultarListado(ShiftQueryModel queryModel)
        {
            return ObtenerQueryable(queryModel).ToList();
        }

        private IQueryable<Shift> ObtenerQueryable(ShiftQueryModel queryModel)
        {
            var query = context.Shift.AsQueryable();

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

            return query;
        }
    }
}
 