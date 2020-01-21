using System;
using System.Linq;

namespace Invoices.Model
{
	public class Contract
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public string ContractType { get; set; }
		public string ContractAcquisitionType { get; set; }
		public string EvaluationCriterion { get; set; }
		public string FundingSource { get; set; }
		public DateTime Date { get; set; }
		public decimal CreditedSum { get; set; }
		public Company Supplier { get; set; }
		public Company Acquirer { get; set; }
		public string Object { get; set; }
		public string CPV { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalValueExclusive { get; set; }
		public decimal TotalValueInclusive { get; set; }
		public decimal Guarantee { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Addendum Addendum { get; set; }
		public string Terms { get; set; }

		public decimal InvoicedValueExclusive
		{
			get
			{
				using (var context = new InvoicesContext())
				{
					return context
						.GetInvoicesByContractId(Id)
						.Sum(i => i.InvoicedValueExclusive);
				}
			}
		}

		public decimal InvoicedValueInclusive
		{
			get
			{
				using (var context = new InvoicesContext())
				{
					return context
						.GetInvoicesByContractId(Id)
						.Sum(i => i.InvoicedValueInclusive);
				}
			}
		}

		public Contract()
		{
			Date = DateTime.Now;
		}

		public override string ToString()
		{
			return Number.ToString();
		}
	}
}
