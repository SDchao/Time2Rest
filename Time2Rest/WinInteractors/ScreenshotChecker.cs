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
            logger.Info("Starting Screenshot Checker Task");
            stop = false;
            Task.Run(() =>
            {
                while (!stop)
                {
                    Thread.Sleep(CheckInterval * 1000);
                    IntPtr nowHwnd = GetForegroundWindow();

                    if (FullscreenDetector.IsDesktop(nowHwnd))
                        continue;

                    Bitmap rawImg = GetScreenshot(nowHwnd);
                    Bitmap nowImg = null;
                    if (rawImg != null)
                        nowImg = new Bitmap(rawImg, new Size(rawImg.Size.Width / 10, rawImg.Size.Height / 10));
                    rawImg?.Dispose();
                    if (nowHwnd != lastHwnd)
                    {
                        if (lastHwnd != IntPtr.Zero)
                        {
                            if (stop)
                                return;
                            logger.Info("Foreground changed, Invoking");
                            onChanged?.Invoke();
                        }

                        lastHwnd = nowHwnd;
                    }
                    else
                    {
                        logger.Debug("Checking screenshot");
                        if (!CompareColor(nowImg, lastImg))
                        {
                            if (stop)
                                return;
                            logger.Info("Foreground Screenshot changed, Invoking");
                            onChanged?.Invoke();
                        }

                    }
                    lastImg?.Dispose();
                    lastImg = nowImg;
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
            try
            {
                RECT rect = new RECT();
                var result = GetWindowRect(hwnd, ref rect);
                if (!result)
                    throw new Exception();

                Rectangle bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

                Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
                }
                return bitmap;
            }
            catch
            {
                logger.Warn("Unable to screenshot");
                return null;
            }
        }

        private int Grayscale(Color c)
        {
            return (int)(0.3 * c.R + 0.59 * c.G + 0.11 * c.B);
        }

        private bool CompareColor(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            for (int j = 0; j < b1.Height; j++)
            {
                for (int i = 0; i < b1.Width; i++)
                {
                    int b1B = Grayscale(b1.GetPixel(i, j));
                    int b2B = Grayscale(b2.GetPixel(i, j));
                    int diff = Math.Abs(b1B - b2B);

                    if (diff > 120)
                        return false;
                }
            }
            return true;
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
