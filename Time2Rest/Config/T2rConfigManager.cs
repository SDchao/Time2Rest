using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Time2Rest.Config
{
    class T2rConfigManager
    {
        public static void WriteConfig(T2rConfig config)
        {
            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter("config.json"))
                {
                    sw.Write(json);
                }
            }
            catch (JsonSerializationException)
            {
                throw new ArgumentException("Unable to convert config into json");
            }
            catch (IOException)
            {
                throw new IOException("Unable to write config.json");
            }
        }

        public static T2rConfig ReadConfig()
        {
            try
            {
                string json = "";
                using (StreamReader sr = new StreamReader("config.json"))
                {
                    json = sr.ReadToEnd();
                }

                T2rConfig config = JsonConvert.DeserializeObject<T2rConfig>(json);
                return config;
            }
            catch (FileNotFoundException)
            {
                var config = new T2rConfig();
                WriteConfig(config);
                return config;
            }
            catch (IOException)
            {
                throw new IOException("Unable to read config.json");
            }
            catch (JsonSerializationException)
            {
                throw new ArgumentException("Unable to convert json into config");
            }
        }
    }
}
