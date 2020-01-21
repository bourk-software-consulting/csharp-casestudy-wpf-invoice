using System;

namespace Invoices.Model
{
	public class Company : IComparable
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Address Address { get; set; }
		public string TradeRegisterNumber { get; set; }
		public string FiscalCode { get; set; }

		public Company()
		{
			Address = new Address();
		}

		public override string ToString()
		{
			return Name;
		}

		public int CompareTo(object other)
		{
			return String.Compare(ToString(), other.ToString());
		}
	}
}
