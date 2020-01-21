using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Invoices.Model;
using Invoices.Repository;
using Invoices.View.Main;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Invoices.View
{
    public partial class MainWindow : Window, Invoices.View.Main.IMainWindow
	{
	    private IMainWindowActionListener actionListener;
		public MainWindow()
		{
			
		    actionListener = new MainWindowActionMediator(this, new InvoicesContext());
			InitializeComponent();
		}

		private void RefreshCompanies()
		{
			using (var context = new InvoicesContext())
				dgCompanies.ItemsSource = context.GetCompanies();
		}

		private void RefreshContracts(){
			using (var context = new InvoicesContext())
				dgContracts.ItemsSource = context.GetContracts();
		}

		private void RefreshInvoices()
		{
			using (var context = new InvoicesContext())
				dgInvoices.ItemsSource = context.GetInvoices();
		}

        public void ListActions(IEnumerable<Transaction> list)
        {
            

        }

        private void dgCompanies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var company = dgCompanies.SelectedItem as Company;

			if (company != null)
			{
				new CompanyWindow(company).ShowDialog();
				RefreshCompanies();
			}
		}

		private void dgContracts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var contract = dgContracts.SelectedItem as Contract;

			if (contract != null)
			{
				new ContractWindow(contract).ShowDialog();
				RefreshContracts();
			}
		}

		private void dgInvoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var invoice = dgInvoices.SelectedItem as Invoice;

			if (invoice != null)
			{
				new InvoiceWindow(invoice).ShowDialog();
				RefreshInvoices();
			}
		}

		private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.OriginalSource is TabControl)
			{
				if (tiCompanies.IsSelected)
					RefreshCompanies();
				else if (tiContracts.IsSelected)
					RefreshContracts();
				else if (tiInvoices.IsSelected)
					RefreshInvoices();
			}
		}

		private void AddCompany_Click(object sender, RoutedEventArgs e)
		{
			new CompanyWindow().ShowDialog();
			RefreshCompanies();
		}

		private void AddContract_Click(object sender, RoutedEventArgs e)
		{
			new ContractWindow().ShowDialog();
			RefreshContracts();
		}

		private void AddInvoice_Click(object sender, RoutedEventArgs e)
		{
			new InvoiceWindow().ShowDialog();
			RefreshInvoices();
		}
        
		private void ExportCompanies_Click(object sender, RoutedEventArgs e)
		{
			ExportDataGrid<Company>(dgCompanies);
		}

		private void ExportContracts_Click(object sender, RoutedEventArgs e)
		{
			ExportDataGrid<Contract>(dgContracts);
		}

		private void ExportInvoices_Click(object sender, RoutedEventArgs e)
		{
			ExportDataGrid<Invoice>(dgInvoices);
		}

		private static void ExportDataGrid<T>(DataGrid dataGrid)
		{
			var sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv|All Files (*.*)|*.*" };
			if (sfd.ShowDialog() == true)
				using (var writer = new StreamWriter(sfd.OpenFile(), System.Text.Encoding.UTF8))
				{
					var columnGetters = new List<Func<object, object>>();

					foreach (var column in dataGrid.Columns)
					{
						writer.Write("\"{0}\",", column.Header);

						columnGetters.Add(BuildPropertyGetter(column.SortMemberPath, typeof(T)));
					}
					writer.WriteLine();

					foreach (var value in dataGrid.ItemsSource)
					{
						foreach (var getter in columnGetters)
							writer.Write("\"{0}\",", getter(value));
						
						writer.WriteLine();
					}
				}
		}

		private static Func<object, object> BuildPropertyGetter(string path, Type type)
		{
			PropertyInfo property;
			var pos = path.IndexOf('.');

			if (pos > 0)
				property = type.GetProperty(path.Substring(0, pos));
			else
				property = type.GetProperty(path);

			if (pos > 0)
			{
				var innerGetter = BuildPropertyGetter(path.Substring(pos + 1), property.PropertyType);
				return o => innerGetter(property.GetValue(o, null));
			}
			return o => property.GetValue(o, null);
		}

	    private void DeleteClicked(object sender, RoutedEventArgs e)
	    {
            var selectedInvoice = dgInvoices.SelectedItem as Invoice;
            actionListener.DeleteInvoiceClick(selectedInvoice);
	    }

	    void IMainWindow.RefreshInvoices()
	    {
	        RefreshInvoices();
	    }
	}
}
