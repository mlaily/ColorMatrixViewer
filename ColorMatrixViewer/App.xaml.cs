using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ColorMatrixViewer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseWheelEvent, new MouseWheelEventHandler(OnPreviewMouseDown));

			base.OnStartup(e);
		}

		static void OnPreviewMouseDown(object sender, MouseWheelEventArgs e)
		{
			Trace.WriteLine("Clicked!!");
		}
	}
}
