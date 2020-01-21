using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Invoices
{
	public static class Extensions
	{
		public static bool IsValid(this DependencyObject source)
		{
			return !Validation.GetHasError(source) &&
				LogicalTreeHelper
					.GetChildren(source)
					.OfType<DependencyObject>()
					.All(c => IsValid(c));
		}
	}
}
