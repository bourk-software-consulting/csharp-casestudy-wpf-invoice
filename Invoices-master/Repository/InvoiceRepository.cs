using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invoices.Model;

namespace Invoices.Repository
{
    public class InvoiceRepository : GenericRepository<InvoicesContext,Invoice>
    {
        public InvoiceRepository(InvoicesContext entities)
            : base(entities)
        {

        }

        public override void Delete(Invoice invoice)
        {
            var query = FindBy(i => i.Id.Equals(invoice.Id));
            var invoiceToDelete = query.FirstOrDefault();
            if(invoiceToDelete != null)
                base.Delete(invoiceToDelete);
            base.Save();
        }

        public void DeleteAll()
        {
            foreach(var invoice in GetAll())
                Delete(invoice);
        }
    }
}
