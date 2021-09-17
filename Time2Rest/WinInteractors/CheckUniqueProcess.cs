using System.Diagnostics;

namespace Time2Rest.WinInteractors
{
    internal class CheckUniqueProcess
    {
        public static bool CheckUnique()
        {
            //string name = Process.GetCurrentProcess().MainModule.FileName;
            string name = Process.GetCurrentProcess().ProcessName;
            int id = Process.GetCurrentProcess().Id;
            Process[] prc = Process.GetProcesses();
            foreach (Process pr in prc)
            {
                try
                {
                    if ((name == pr.ProcessName) && (pr.Id != id))
                    {
                        return true;
                    }
                }
                catch { }
            }
            return false;
        }
    }
}