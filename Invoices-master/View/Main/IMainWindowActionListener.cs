using Invoices.Model;

namespace Invoices.View.Main
{
    public interface IMainWindowActionListener
    {
        void DeleteInvoiceClick(Invoice invoice);
    }
}