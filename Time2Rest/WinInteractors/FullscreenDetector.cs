using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Time2Rest.WinInteractors
{
    internal class FullscreenDetector
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public static bool IsForegroundFullScreen()
        {
            return IsForegroundFullScreen(null);
        }

        public static bool IsForegroundFullScreen(Screen screen)
        {
            if (screen == null)
            {
                screen = Screen.PrimaryScreen;
            }
            RECT rect = new RECT();
            IntPtr foreHwnd = GetForegroundWindow();
            if (IsDesktop(foreHwnd))
                return false;
            GetWindowRect(new HandleRef(null, foreHwnd), ref rect);
            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(screen.Bounds);
        }

        public static bool IsDesktop(IntPtr hWnd)
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            if (GetClassName(hWnd, stringBuilder, 256) == 0)
            {
                string cName = stringBuilder.ToString();
                if (cName == "Progman" || cName == "WorkerW")
                    return true;
            }

            return false;
        }
    }
}