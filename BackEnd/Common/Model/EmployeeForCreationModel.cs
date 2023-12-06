namespace Common.Model
{
    public class EmployeeForCreationModel
    {
		public string Nombre {get; set; }
		public string Apellido {get; set; }
		public string Dni {get; set; } 
		public string Numero {get; set; }
		public string Direccion {get; set; } 
		public string Email {get; set; } 
		public decimal? Salario {get; set; } 
		public string Posicion {get; set; } 
    }
}