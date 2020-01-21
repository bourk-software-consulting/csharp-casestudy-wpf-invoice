using System.Windows;
using System.Windows.Controls;
using Invoices.Model;

namespace Invoices.Editors
{
	public partial class ContractEditor : UserControl
	{
		public static readonly RoutedEvent AddendumOpenEvent = EventManager.RegisterRoutedEvent("AddendumOpen", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ContractEditor));

		public event RoutedEventHandler AddendumOpen
		{
			add { AddHandler(AddendumOpenEvent, value); }
			remove { RemoveHandler(AddendumOpenEvent, value); }
		}

		public ContractEditor()
		{
			InitializeComponent();
		}

		private void RaiseAddendumOpen()
		{
			RaiseEvent(new RoutedEventArgs(AddendumOpenEvent));
		}

		private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var contract = DataContext as Contract;
			if (contract == null)
				return;

			if (contract.Addendum == null)
				btnAddendum.Content = "Create";
			else
				btnAddendum.Content = "Open";
		}

		private void btnAddendum_Click(object sender, RoutedEventArgs e)
		{
			RaiseAddendumOpen();
		}
	}
}
