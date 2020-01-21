using System.Collections.Generic;
using System.Linq;
using Invoices.Model;

namespace Invoices.View.Main
{
    public interface IMainWindow
    {
        void RefreshInvoices();
        void ListActions(IEnumerable<Transaction> list);
    }
}