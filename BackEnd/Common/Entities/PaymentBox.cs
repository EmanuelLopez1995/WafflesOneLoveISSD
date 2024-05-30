namespace Common.Entities
{
	public class PaymentBox
	{

		public int Id { get; set; }
		public float? InitialActive { get; set; }
		public float? CashWitdrawal { get; set; }
		public float? InitialImport { get; set; }
		public float? FinalImport { get; set; }

		//cambiar todo a  que sea nulo
	}
}