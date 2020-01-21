using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Invoices.Model;
using Invoices.Repository;
using Invoices.View.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTestProject2
{
    
    /// <summary>
    /// Summary description for ListActionsTests
    /// </summary>
    [TestClass]
    public class ListActionsTests
    {
        [TestMethod]
        public void TestListActions()
        {
            //Arrange 
            Invoice invoice;
            Transaction action;
            using (var context = new InvoicesContext())
            {
                var invoicesInitializer = new InvoicesInitializer();
                Database.SetInitializer(invoicesInitializer);
                context.Database.Initialize(true);
                invoice = invoicesInitializer.Invoice;
            }

            IGenericRepository<Transaction> repository = new GenericRepository<InvoicesContext,Transaction>(new InvoicesContext());

            Mock<IMainWindow> mockView = new Mock<IMainWindow>();
            MainWindowActionMediator mediator = new MainWindowActionMediator(mockView.Object, new InvoicesContext());

            //Act
            mediator.ListActions();
            
            //Assert
            
            mockView.Verify(m => m.ListActions(It.IsAny<IEnumerable<Transaction>>()));
            
        }
    }
}