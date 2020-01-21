using Invoices.Model;
using Invoices.Repository;

namespace Invoices.View.Main
{
    public class MainWindowActionMediator : IMainWindowActionListener
    {
        private readonly IMainWindow _view;
        private readonly IGenericRepository<Invoice> _invoiceRepository;
        public MainWindowActionMediator(IMainWindow view, InvoicesContext invoiceContext)
        {
            this._view = view;
            _invoiceRepository = new InvoiceRepository(invoiceContext);
        }

        public void DeleteInvoiceClick(Invoice invoice)
        {
            _invoiceRepository.Delete(invoice);
            _view.RefreshInvoices();
        }

        public void ListActions()
        {
            _view.ListActions(null);
        }
    }
}