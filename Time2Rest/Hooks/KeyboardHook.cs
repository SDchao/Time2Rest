using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using NLog;
namespace Time2Rest.Hooks
{
    class KeyboardHook
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);


        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;

        static int hKeyboardHook = 0;
        HookProc KeyboardHookProcDelegate;
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                Logger.Debug("Key: {0} {1}", wParam, lParam);
            }

            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }


        public void StartHook()
        {
            Logger.Debug("Starting Hook");
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcDelegate = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);

                Logger.Info("Hooked Keyboard");

                if (hKeyboardHook == 0)
                {
                    StopHook();
                    Logger.Error("Unable to start hook");
                    throw new Exception("Hook failed");
                }
            }
        }

        public void StopHook()
        {
            Logger.Debug("Stoping Hook");
            if (hKeyboardHook != 0)
            {
                bool retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
                Logger.Info("Unhooked keyboard");
                if (!retKeyboard)
                {
                    Logger.Error("Unable to unhook");
                    throw new Exception("Unhook failed");
                }
            }
        }
    }
}
