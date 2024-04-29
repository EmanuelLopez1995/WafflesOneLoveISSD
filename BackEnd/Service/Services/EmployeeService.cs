using Common.Entities;
using Common.Interfaces.Service;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;

namespace Service.Services
{
    public class EmployeeService : DataAccessAbstractService, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AckEntity<EmployeeModel> Crear(EmployeeModel model)
        {
            var ack = new AckEntity<EmployeeModel>();
            //if (model.Email != "asdasdas")
            //{
            //    ack.Mensaje = "El Email No Es Valido";
            //    return ack;
            //}

            var empleado = new Employee
            {
                Direction = model.Direccion,
                Dni = model.Dni,
                Email = model.Email,
                LastName = model.Apellido,
                Name = model.Nombre,
                PhoneNumber = model.Numero,
                Position = model.Posicion
            };

            UoW.Employees.Add(empleado);
            UoW.Complete();

            model.Id = empleado.Id;

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }

        public Ack Delete(int id)
        {
            var ack = new Ack();

            var employee = UoW.Employees.Obtener(id);
            if (employee == null)
            {
                ack.Mensaje = "El Empleado No Existe";
                return ack;
            }

            UoW.Employees.Remove(employee);
            UoW.Complete();

            ack.Exito = true;
            return ack;
        }

        public EmployeeModel Obtener(int id)
        {
            var employee = UoW.Employees.Obtener(id);
            if (employee == null)
                return null;

            return new EmployeeModel
            {
                Apellido = employee.LastName,
                Nombre = employee.Name,
                Direccion = employee.Direction,
                Dni = employee.Dni,
                Email = employee.Email,
                Id = employee.Id,
                Numero = employee.PhoneNumber,
                Posicion = employee.Position,
                
            };
        }

        public List<EmployeeModel> GetAll()
        {
            var list = UoW.Employees.GetAll();
            return list.Select(employee => new EmployeeModel
            {
                Apellido = employee.LastName,
                Nombre = employee.Name,
                Direccion = employee.Direction,
                Dni = employee.Dni,
                Email = employee.Email,
                Id = employee.Id,
                Numero = employee.PhoneNumber,
                Posicion = employee.Position,
                
            }).ToList();
        }

        public AckEntity<EmployeeModel> Update(EmployeeModel model)
        {
            var ack = new AckEntity<EmployeeModel>();

            var employee = UoW.Employees.Obtener(model.Id);
            if (employee == null)
            {
                ack.Mensaje = "El Empleado No Existe";
                return ack;
            }

            employee.Direction = model.Direccion;
            employee.Dni = model.Dni;
            employee.Email = model.Email;
            employee.LastName = model.Apellido;
            employee.Name = model.Nombre;
            employee.PhoneNumber = model.Numero;
            employee.Position = model.Posicion;

            UoW.Complete();

            ack.Exito = true;
            ack.Entity = model;

            return ack;
        }
    }
}