using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Invoices.Model;
using Invoices.View.Main;
using Moq;
using TechTalk.SpecFlow;

namespace Invoices_Specs.DeleteInvoice
{
    [Binding]
    public class DeleteInvoiceSteps
    {
        private IMainWindowActionListener _mainWindow;
        private Invoice _invoice;
        private Mock<IMainWindow> mockMainWindow;
        private Mock<InvoicesContext> mockInvoiceContext;
        private IQueryable<Invoice> data;
        private Mock<DbSet<Invoice>> mockSet;
        private Invoice invoice;

        [Given]
        public void GivenIHaveAnExistingInvoice()
        {
            // documentation about mocking DbSets - https://msdn.microsoft.com/en-us/data/dn314429.aspx?f=255&MSPPError=-2147217396
            invoice = new Invoice {Description = "AAA"};
            data = new List<Invoice>
            {
                invoice   
            }.AsQueryable();
            
            mockSet = new Mock<DbSet<Invoice>>();
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockMainWindow = new Mock<IMainWindow>();
            mockInvoiceContext = new Mock<InvoicesContext>();
            _mainWindow = new MainWindowActionMediator(mockMainWindow.Object,mockInvoiceContext.Object);
            _invoice = new Invoice();
            mockInvoiceContext.Setup(m => m.Invoices).Returns(mockSet.Object);
            mockInvoiceContext.Setup(m => m.Set<Invoice>()).Returns(mockSet.Object);
        }

        [When]
        public void WhenIPressDeleteOnThisInvoice()
        {
            _mainWindow.DeleteInvoiceClick(_invoice);
        }

        [Then]
        public void ThenTheInvoiceShouldNotAppearInTheList()
        {
            mockSet.Verify(m=> m.Remove(invoice));
        }
    }
}
