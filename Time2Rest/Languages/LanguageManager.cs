using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NLog;
using System.Reflection;

namespace Time2Rest.Languages
{
    class LanguageManager
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
                return new ResXResourceSet(defaultPath);
            }
        }
    }
}
