using System;

namespace Invoices.Model
{
	public class Addendum
	{
		public virtual int Id { get; set; }
		public virtual int Number { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string Content { get; set; }

		public Addendum()
		{
			Date = DateTime.Now;
		}

		public override string ToString()
		{
			return String.Format("{0}/{1:d}", Number, Date);
		}
	}
}
