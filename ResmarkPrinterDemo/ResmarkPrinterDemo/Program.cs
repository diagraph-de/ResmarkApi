using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using ResmarkPrinterGroupDemo.Properties;

namespace ResmarkPrinterGroupDemo;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        if (!string.IsNullOrWhiteSpace(Settings.Default.Language))
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
        }
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new GroupMainForm());
    }
}