using Common.Interfaces.Repository;
using Common.Repository;
using Data.Repository;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context context;

        public UnitOfWork(Context context)
        {
            this.context = context;
            Employees = new EmployeeRepository(context);
            EmployeeShifts = new EmployeeShiftRepository(context);
            Suppliers=new SuppliersRepository(context);
            Shifts = new ShiftRepository(context);
            PaymentBox = new PaymentBoxRepository(context);
            Salary = new SalaryRepository(context);
            PaymentBoxInitialActive=new PaymentBoxInitialActiveRepository(context);
            Stock=new StockRepository(context);

        }

        public IEmployeeRepository Employees { get; private set; }
        public IEmployeeShiftRepository EmployeeShifts { get; private set; }
        public ISuppliersRepository Suppliers { get; private set; }
        public IShiftRepository Shifts { get; private set; }
        public IPaymentBoxRepository PaymentBox { get; private set; }
        public IPaymentBoxInitialActiveRepository PaymentBoxInitialActive { get; private set; }

        public ISalaryRepository Salary { get; private set; }
        public IStockRepository Stock { get; private set; }




        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }

       
    }
}
