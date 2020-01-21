using System;

namespace Invoices.Model
{
	public class Address : IComparable
	{
		public int Id { get; set; }
		public string Street { get; set; }
		public string Number { get; set; }
		public string Building { get; set; }
		public string Entrance { get; set; }
		public int ApartmentNumber { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string County { get; set; }

		public override string ToString()
		{
			return String.Format("{0}, {1}, {2}, {3}", Street, Number, City, County);
		}

		public int CompareTo(object other)
		{
			return String.Compare(ToString(), other.ToString());
		}
	}
}
