using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Invoices.Model;
using Invoices.Repository;
using Invoices.View.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Invoices_Tests
{
    [TestClass]
    public class InvoicesTests
    {
        [TestMethod]
        public void TestRemoveTheInvoiceFromDatabase()
        {
            //Arrange 
            Invoice invoice;
            using (var context = new InvoicesContext())
            {
                var invoicesInitializer = new InvoicesInitializer();
                Database.SetInitializer(invoicesInitializer);
                context.Database.Initialize(true);
                invoice = invoicesInitializer.Invoice;
            }

            IGenericRepository<Invoice> repository = new InvoiceRepository(new InvoicesContext());

            Mock<IMainWindow> mockView = new Mock<IMainWindow>();
            MainWindowActionMediator mediator = new MainWindowActionMediator(mockView.Object, new InvoicesContext());

            //Act
            mediator.DeleteInvoiceClick(invoice);
            //repository.Delete(invoice);
            
            //Assert
            var listOfInvoice = repository.FindBy(i => (i.Id.Equals(invoice.Id)));
            Assert.AreEqual(0, listOfInvoice.Count());
            mockView.Verify(m=>m.RefreshInvoices());
        }

        [TestMethod]
        public void LoadTestRemoveTheInvoiceFromDatabase()
        {
            //Arrange 
            var contract = new Contract()
            {
                Object = "blahblahtesting"
            };

            var invoice = new Invoice()
            {
                Number = 321,
                Contract = contract
            };
            IGenericRepository<Invoice> repository = new InvoiceRepository(new InvoicesContext());

            repository.Add(invoice);
            repository.Save();


            //Act
            repository.Delete(invoice);

            //Assert
            var listOfInvoice = repository.FindBy(i => (i.Id.Equals(invoice.Id)));
            Assert.AreEqual(0, listOfInvoice.Count());
        }

        // Moq Demo
        [TestMethod]
        public void TestMainWidowDeleteOneInvoice()
        {
            var moqInvoiceRepository = new Mock<IInvoiceRepository>();
            var invoiceActionListener = new InvoiceActionListener(moqInvoiceRepository.Object);

            var invoice = new Invoice();
            invoiceActionListener.DeleteClicked(invoice);
            
            moqInvoiceRepository.Verify(r => r.Delete(invoice));
        }
    }
    
    // for the Mock Demo
    public class InvoiceActionListener
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceActionListener(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public void DeleteClicked(Invoice invoice)
        {
            _invoiceRepository.Delete(invoice);
        }
    }
}
