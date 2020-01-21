using System;

namespace Invoices.Model
{
	public class Invoice
	{
		public int Id { get; set; }
		public string Series { get; set; }
		public long Number { get; set; }
		public DateTime Date { get; set; }
		public int NIR { get; set; }
		public int PV { get; set; }
		public Contract Contract { get; set; }
		public string Description { get; set; }
		public decimal InvoicedValueInclusive { get; set; }
		public decimal InvoicedValueExclusive { get; set; }

		public Invoice()
		{
			Date = DateTime.Now;
		}
	}
}
