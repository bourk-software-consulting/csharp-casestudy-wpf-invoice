using System.Windows;
using System.Windows.Controls;
using Invoices.Model;

namespace Invoices.Editors
{
	public class SupplierChangedEventArgs : RoutedEventArgs
	{
		public Company Supplier { get; set; }

		public SupplierChangedEventArgs(RoutedEvent routedEvent, Company supplier)
			: base()
		{
			Supplier = supplier;
		}
	}

	public partial class InvoiceEditor : UserControl
	{
		public static readonly RoutedEvent SupplierChangedEvent =
			EventManager.RegisterRoutedEvent("SupplierChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InvoiceEditor));

		public InvoiceEditor()
		{
			InitializeComponent();
		}

		private void RaiseSupplierChangedEvent(Company supplier)
		{
			RaiseEvent(new SupplierChangedEventArgs(SupplierChangedEvent, supplier));
		}

		private void Supplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//if (e.AddedItems.Count == 1)
			//    RaiseSupplierChangedEvent(e.AddedItems[0] as Company);
		}
	}
}
