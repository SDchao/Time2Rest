using System;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Time2Rest.Languages
{
    internal class LanguageManager
    {
        private readonly static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        private readonly static string defaultPath = basePath + @"Languages\" + "en" + ".resx";

        public static ResXResourceSet GetLangRes()
        {
            string cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            logger.Info("Current culture name : {0}", cultureName);
            string expectFilePath = basePath + @"Languages\" + cultureName + ".resx";
            if (File.Exists(expectFilePath))
            {
                logger.Info("Loading res: {0}", expectFilePath);
                return new ResXResourceSet(expectFilePath);
            }
            else
            {
                logger.Warn("Res for current language not found, using default");

                if (!File.Exists(defaultPath))
                {
                    logger.Fatal("Unable to find default language!");
                    MessageBox.Show("Unable to load language file, make sure Languages Folder is here!", "FATAL_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                return new ResXResourceSet(defaultPath);
            }
        }
    }
}