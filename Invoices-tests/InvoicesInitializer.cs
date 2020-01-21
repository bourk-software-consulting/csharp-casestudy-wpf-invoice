using System;
using System.Data.Entity;

namespace Invoices.Model
{
	public class InvoicesInitializer : DropCreateDatabaseAlways<InvoicesContext>
	{
	    private readonly int _invoiceNumber;
	    private Invoice _invoice;

	    public InvoicesInitializer()
	    {
	        
	    }

	    public Invoice Invoice
	    {
	        get { return _invoice; }
	    }

	    protected override void Seed(InvoicesContext context)
		{
			var address = new Address { Street = "Strada 1", Number = "2a", Building = "A1", ApartmentNumber = 4 };
			var company = new Company { Name = "Firma 1", Address = address, FiscalCode = "123", TradeRegisterNumber = "12" };
			var contract = new Contract { Date = DateTime.Now, Supplier = company, Acquirer = company};
			_invoice = new Invoice { Contract = contract, Date = DateTime.Now };

			context.Addresses.Add(address);
			context.Companies.Add(company);
			context.Contracts.Add(contract);
			context.Invoices.Add(_invoice);
			context.SaveChanges();
		}
	}
}
