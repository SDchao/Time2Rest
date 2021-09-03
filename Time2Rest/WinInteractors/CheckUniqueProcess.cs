using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            string name = Process.GetCurrentProcess().ProcessName;
            int id = Process.GetCurrentProcess().Id;
            Process[] prc = Process.GetProcesses();
            foreach (Process pr in prc)
            {
                if ((name == pr.ProcessName) && (pr.Id != id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}