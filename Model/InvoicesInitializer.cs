using System;
using System.Data.Entity;

namespace Invoices.Model
{
	public class InvoicesInitializer : DropCreateDatabaseIfModelChanges<InvoicesContext>
	{
		protected override void Seed(InvoicesContext context)
		{
			//var address1 = new Address { Street = "Strada 1", Number = "2a", Building = "A1", ApartmentNumber = 4 };
			//var address2 = new Address { Street = "Strada 2", Number = "3", Building = "B2", ApartmentNumber = 5 };

			//var company1 = new Company { Name = "Firma 1", Address = address1, FiscalCode = "123", TradeRegisterNumber = "12" };
			//var company2 = new Company { Name = "Firma 2", Address = address2, FiscalCode = "222", TradeRegisterNumber = "20" };

			//var contract1 = new Contract { Date = DateTime.Now, Supplier = company1, Acquirer = company2 };
			//var contract2 = new Contract { Date = DateTime.Now, Supplier = company2, Acquirer = company1 };

			//var invoice1 = new Invoice { Contract = contract1, Date = DateTime.Now };
			//var invoice2 = new Invoice { Contract = contract2, Date = DateTime.Now };

			//context.Addresses.Add(address1);
			//context.Addresses.Add(address2);

			//context.Companies.Add(company1);
			//context.Companies.Add(company2);

			//context.Contracts.Add(contract1);
			//context.Contracts.Add(contract2);

			//context.Invoices.Add(invoice1);
			//context.Invoices.Add(invoice2);

			//context.SaveChanges();
		}
	}
}
