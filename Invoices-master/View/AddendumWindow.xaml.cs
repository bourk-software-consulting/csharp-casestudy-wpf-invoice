using System;
using System.Windows;
using System.Windows.Input;
using Invoices.Model;

namespace Invoices.View
{
	public partial class AddendumWindow : Window
	{
		private InvoicesContext context;

		public AddendumWindow(Addendum addendum, InvoicesContext context)
		{
			InitializeComponent();

			this.context = context;

			DataContext = addendum;
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
	}
}
