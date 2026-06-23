using System;
using System.Windows.Forms;

namespace CrimsonTrainer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Enable visual styles and compatible text rendering for WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            Application.Run(new MainForm());
        }
    }
}
