using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time2Rest.WinInteractors
{
    class StartupManager
    {
        static RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public static void SetStartup(bool needStartup)
        {
            if (needStartup)
            {
                rkApp.SetValue("Time2Rest",'"' + Application.ExecutablePath.Replace("/", "\\") + '"');
            }
            else
            {
                rkApp.DeleteValue("Time2Rest", false);
            }
        }
    }
}
