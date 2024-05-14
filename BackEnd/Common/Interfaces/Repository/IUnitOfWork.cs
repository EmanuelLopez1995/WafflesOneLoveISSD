using Common.Interfaces.Repository;

namespace Common.Repository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IEmployeeShiftRepository EmployeeShifts { get; }
        ISuppliersRepository Suppliers { get; }
        IShiftRepository Shifts { get; }
        IPaymentBoxRepository PaymentBox {get;}
        ISalaryRepository Salary { get; }
        IPaymentBoxInitialActiveRepository PaymentBoxInitialActive  { get; }
        IStockRepository Stock { get; }
        int Complete();
    }
}
