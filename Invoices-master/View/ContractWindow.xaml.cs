using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Invoices.Model;

namespace Invoices.View
{
	public partial class ContractWindow : Window
	{
		public static readonly DependencyProperty CompanyListProperty = DependencyProperty.Register(
			"CompanyList",
			typeof(IEnumerable<Company>),
			typeof(ContractWindow));

		public IEnumerable<Company> CompanyList
		{
			get { return GetValue(CompanyListProperty) as IEnumerable<Company>; }
			set { SetValue(CompanyListProperty, value); }
		}

		private InvoicesContext context;

		public ContractWindow(Contract contract = null)
		{
			InitializeComponent();

			context = new InvoicesContext();

			CompanyList = context.GetCompanies();

			if (contract == null)
			{
				contract = context.Contracts.Create();
				context.Contracts.Add(contract);
			}
			else
				contract = context.GetContractById(contract.Id);

			DataContext = contract;
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

		private void ContractEditor_AddendumOpen(object sender, RoutedEventArgs e)
		{
			var contract = DataContext as Contract;
			if (contract == null)
				return;

			if (contract.Addendum == null)
			{
				contract.Addendum = context.Addendums.Create();
				context.SaveChanges();
			}
			new AddendumWindow(contract.Addendum, context).ShowDialog();

			DataContext = null;
			DataContext = contract;
		}
	}
}
