using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using AutoUpdaterDotNET;
using System.Management;
using System.Windows.Input;

namespace FUTPlus
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            {
                FileInfo fis = new FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                if (fis.Length < 14680064)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FUTPlus());
                }
            }
        }
    }
}