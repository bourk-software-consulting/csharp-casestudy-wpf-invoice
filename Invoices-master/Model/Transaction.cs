using System;

namespace Invoices.Model
{
	public class Transaction
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }

		public Transaction()
		{
			Date = DateTime.Now;
		}
	}
}
