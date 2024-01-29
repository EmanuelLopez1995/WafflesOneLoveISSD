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

        int Complete();
    }
}
