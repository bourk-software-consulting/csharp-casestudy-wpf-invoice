using System;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Invoices
{
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement),
				new FrameworkPropertyMetadata(XmlLanguage.GetLanguage("en-EN")));
		}

		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			var sb = new StringBuilder();
			BuildExceptionString(e.Exception, sb);

			MessageBox.Show(sb.ToString());
		}

		private static void BuildExceptionString(Exception e, StringBuilder sb)
		{
			if (e.InnerException != null)
			{
				BuildExceptionString(e.InnerException, sb);
				sb.AppendLine();
			}

			sb.Append(e);
		}
	}
}
