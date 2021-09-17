using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time2Rest.WinInteractors
{
    class ScreenshotChecker
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        private readonly int CheckInterval = 180;
        private Bitmap lastImg = null;
        private IntPtr lastHwnd = IntPtr.Zero;
        private bool stop = false;

        public Action onChanged;

        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (stop)
                        return;

                    IntPtr nowHwnd = GetForegroundWindow();
                    if (nowHwnd != lastHwnd)
                    {
                        logger.Info("Foreground changed, Invoking");
                        lastHwnd = nowHwnd;
                        onChanged?.Invoke();
                    }
                    else
                    {
                        Bitmap nowImg = GetScreenshot(nowHwnd);
                        logger.Debug("Checking screenshot");
                        if (!CompareMemCmp(nowImg, lastImg))
                        {
                            logger.Info("Foreground Screenshot changed, Invoking");
                            onChanged?.Invoke();
                        }
                        lastImg?.Dispose();
                        lastImg = nowImg;
                    }
                    Thread.Sleep(CheckInterval * 1000);
                }
            });
        }

        public void Stop()
        {
            logger.Debug("Stopping Screenshot Checker");
            stop = true;
        }

        private Bitmap GetScreenshot(IntPtr hwnd)
        {
            RECT rect = new RECT();
            var result = GetWindowRect(hwnd, ref rect);
            if (!result)
                return null;

            Rectangle bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
            }
            return bitmap;
        }

        private bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
    }
}
