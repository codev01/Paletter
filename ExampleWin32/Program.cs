using System;
using System.Windows.Forms;

namespace ExampleWin32
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
#if NET6_0_OR_GREATER
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
#else
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
#endif
			Application.Run(new Main());
		}
	}
}