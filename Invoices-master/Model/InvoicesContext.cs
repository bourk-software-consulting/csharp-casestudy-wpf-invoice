using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Invoices.Model
{
    /*
     *   This DbContext breaks the Single Responsibility Principle as it has all the data, not only the Invoices.     *   
     */

	public class InvoicesContext : DbContext
	{
		public DbSet<Addendum> Addendums { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Contract> Contracts { get; set; }
		public virtual DbSet<Invoice> Invoices { get; set; } // needs to virtual so that Moq can override it.

		public InvoicesContext()
		{
			Configuration.ProxyCreationEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public IEnumerable<Addendum> GetAddendums()
		{
			return Addendums
				.ToList();
		}

		public IEnumerable<Company> GetCompanies()
		{
			return Companies
				.Include(c => c.Address)
				.ToList();
		}

		public IEnumerable<Contract> GetContracts()
		{
			return Contracts
				.Include(c => c.Acquirer.Address)
				.Include(c => c.Supplier.Address)
				.Include(c => c.Addendum)
				.OrderByDescending(c => c.Date)
				.ToList();
		}

		public IEnumerable<Invoice> GetInvoices()
		{
			return Invoices
				.Include(i => i.Contract.Acquirer.Address)
				.Include(i => i.Contract.Supplier.Address)
				.Include(i => i.Contract.Addendum)
				.OrderByDescending(i => i.Date)
				.ToList();
		}

		public Addendum GetAddendumById(int id)
		{
			return Addendums
				.Single(a => a.Id == id);
		}

		public Company GetCompanyById(int id)
		{
			return Companies
				.Include(c => c.Address)
				.Single(c => c.Id == id);
		}

		public Contract GetContractById(int id)
		{
			return Contracts
				.Include(c => c.Acquirer.Address)
				.Include(c => c.Supplier.Address)
				.Include(c => c.Addendum)
				.Single(c => c.Id == id);
		}

		public Invoice GetInvoiceById(int id)
		{
			return Invoices
				.Include(i => i.Contract.Acquirer.Address)
				.Include(i => i.Contract.Supplier.Address)
				.Include(i => i.Contract.Addendum)
				.Single(i => i.Id == id);
		}

		public IEnumerable<Contract> GetContractsBySupplierId(int supplierId)
		{
			return Contracts
				.Include(c => c.Acquirer.Address)
				.Include(c => c.Supplier.Address)
				.Include(c => c.Addendum)
				.Where(c => c.Supplier.Id == supplierId)
				.ToList();
		}

		public IEnumerable<Invoice> GetInvoicesByContractId(int contractId)
		{
			return Invoices
				.Include(i => i.Contract.Acquirer.Address)
				.Include(i => i.Contract.Supplier.Address)
				.Include(i => i.Contract.Addendum)
				.ToList();
		}
	}
}
