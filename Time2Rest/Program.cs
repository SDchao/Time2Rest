using NLog;
using System;
using System.Windows.Forms;

namespace Time2Rest
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Logger
            var config = new NLog.Config.LoggingConfiguration();
            var fileTarget = new NLog.Targets.FileTarget("logfile")
            {
                Layout = @"${longdate} ${logger} ${message}${exception:format=ToString}",
                FileName = @"${basedir}/Logs/logfile.txt",
                KeepFileOpen = true,
                DeleteOldFileOnStartup = true,
                Encoding = System.Text.Encoding.UTF8
            };

            var consoleTarget = new NLog.Targets.ConsoleTarget("logconsole")
            {
                Layout = "[${level}] ${longdate} ${callsite}: ${message}"
            };

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AlertForm());
        }
    }
}