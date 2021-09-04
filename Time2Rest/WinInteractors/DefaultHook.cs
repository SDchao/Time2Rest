using NLog;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Time2Rest.WinInteractors
{
    internal class DefaultHook
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        protected delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;

        private static int hKeyboardHook = 0;
        private static int hMouseHook = 0;
        private HookProc KeyboardHookProcDelegate;
        private HookProc MouseHookProcDelegate;
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public event Action OnOperation;

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //Logger.Debug("Keyboard: {0} {1}", wParam, lParam);
                Operation();
            }

            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // wParam = 512: No Mouse button clicked
            if (nCode >= 0)
            {
                //Logger.Debug("Mouse: {0} {1}", wParam, lParam);
                Operation();
            }

            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        protected virtual void Operation()
        {
            OnOperation?.Invoke();
        }

        public void StartHook()
        {
            Logger.Debug("Starting Hook");
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcDelegate = new HookProc(KeyboardHookProc);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                if (hKeyboardHook == 0)
                {
                    StopHook();
                    Logger.Error("Unable to hook keyboard");
                    throw new Exception("Unable to hook keyboard");
                }
                Logger.Info("Hooked Keyboard");

                MouseHookProcDelegate = new HookProc(MouseHookProc);
                hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseHookProcDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                if (hMouseHook == 0)
                {
                    StopHook();
                    Logger.Error("Unable to hook mouse");
                    throw new Exception("Unable to hook mouse");
                }
                Logger.Info("Hooked Mouse");
            }
        }

        public void StopHook()
        {
            Logger.Debug("Stopping Hook");
            if (hKeyboardHook != 0)
            {
                bool retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
                Logger.Info("Unhooked keyboard");
                if (!retKeyboard)
                {
                    Logger.Error("Unable to unhook keyboard");
                    throw new Exception("Unhook keyboard failed");
                }
            }

            if (hMouseHook != 0)
            {
                bool retMouse = UnhookWindowsHookEx(hMouseHook);
                hMouseHook = 0;
                Logger.Info("Unhooked mouse");
                if (!retMouse)
                {
                    Logger.Error("Unable to unhook mouse");
                    throw new Exception("Unhook mouse failed");
                }
            }
        }
    }
}