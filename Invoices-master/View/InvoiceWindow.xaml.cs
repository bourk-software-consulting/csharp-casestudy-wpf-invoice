using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Invoices.Model;

namespace Invoices.View
{
	public partial class InvoiceWindow : Window
	{
		public static readonly DependencyProperty CompanyListProperty = DependencyProperty.Register(
			"CompanyList",
			typeof(IEnumerable<Company>),
			typeof(InvoiceWindow));

		public static readonly DependencyProperty ContractListProperty = DependencyProperty.Register(
			"ContractList",
			typeof(IEnumerable<Contract>),
			typeof(InvoiceWindow));

		public static readonly DependencyProperty SelectedSupplierProperty = DependencyProperty.Register(
			"SelectedSupplier",
			typeof(Company),
			typeof(InvoiceWindow),
			new PropertyMetadata(SelectedSupplier_Changed));

		public IEnumerable<Company> CompanyList
		{
			get { return GetValue(CompanyListProperty) as IEnumerable<Company>; }
			set { SetValue(CompanyListProperty, value); }
		}

		public IEnumerable<Contract> ContractList
		{
			get { return GetValue(ContractListProperty) as IEnumerable<Contract>; }
			set { SetValue(ContractListProperty, value); }
		}

		public Company SelectedSupplier
		{
			get { return GetValue(SelectedSupplierProperty) as Company; }
			set { SetValue(SelectedSupplierProperty, value); }
		}

		private InvoicesContext context;

		public InvoiceWindow(Invoice invoice = null)
		{
			InitializeComponent();

			context = new InvoicesContext();

			CompanyList = context.GetCompanies();
			ContractList = context.GetContracts();

			if (invoice == null)
			{
				invoice = context.Invoices.Create();
				context.Invoices.Add(invoice);
			}
			else
				invoice = context.GetInvoiceById(invoice.Id);

			if (invoice.Contract != null)
				SelectedSupplier = invoice.Contract.Supplier;

			DataContext = invoice;
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

		private static void SelectedSupplier_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var invoiceWindow = d as InvoiceWindow;
			if (invoiceWindow == null)
				return;

			invoiceWindow.ContractList = invoiceWindow.context.GetContractsBySupplierId((e.NewValue as Company).Id);
		}
	}
}
