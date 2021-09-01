﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using NLog;
namespace Time2Rest.Hooks
{
    class DefaultHook
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);


        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;
        const int WH_MOUSE_LL = 14;

        static int hKeyboardHook = 0;
        static int hMouseHook = 0;
        HookProc KeyboardHookProcDelegate;
        HookProc MouseHookProcDelegate;
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                Logger.Debug("Keyboard: {0} {1}", wParam, lParam);
            }

            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // wParam = 512: No Mouse button clicked
            if (nCode >= 0)
            {
                Logger.Debug("Mouse: {0} {1}", wParam, lParam);
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
            Logger.Debug("Stoping Hook");
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