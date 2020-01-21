using System;
using System.Windows;
using System.Windows.Input;
using Invoices.Model;

namespace Invoices.View
{
	public partial class CompanyWindow : Window
	{
		private InvoicesContext context;

		public CompanyWindow(Company company = null)
		{
			InitializeComponent();

			context = new InvoicesContext();

			if (company == null)
			{
				company = context.Companies.Create();
				context.Companies.Add(company);
			}
			else
				company = context.GetCompanyById(company.Id);
			
			DataContext = company;
		}

		private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = this.IsValid();
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			(e.OriginalSource as FrameworkElement).Focus();

			context.SaveChanges();
			Close();
		}

		private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Close();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			context.Dispose();
		}
	}
}
