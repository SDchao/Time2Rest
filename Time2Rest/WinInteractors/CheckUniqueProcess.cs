using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Time2Rest.Languages;
namespace Time2Rest.WinInteractors
{
    class CheckUniqueProcess
    {
        public static bool CheckUnique()
        {
            string name = Process.GetCurrentProcess().MainModule.FileName;
            int id = Process.GetCurrentProcess().Id;
            Process[] prc = Process.GetProcesses();
            foreach (Process pr in prc)
            {
                try
                {
                    if ((name == pr.MainModule.FileName) && (pr.Id != id))
                    {
                        return true;
                    }
                }
                catch (Win32Exception) { }
            }
            return false;
        }
    }
}