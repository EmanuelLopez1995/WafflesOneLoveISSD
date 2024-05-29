namespace Common.Entities
{
	public class Employee
	{


		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Dni { get; set; }
		public string PhoneNumber { get; set; }
		public string Direction { get; set; }
		public string Email { get; set; }
		public string Position { get; set; }
		public virtual ICollection<EmployeeShift> EmployeeShifts { get; set; } = new HashSet<EmployeeShift>();

		public virtual ICollection<Shift> ShiftsClosed { get; set; } = new HashSet<Shift>();
		public virtual ICollection<Shift> ShiftsOpen { get; set; } = new HashSet<Shift>();
	}
}