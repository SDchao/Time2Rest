using Newtonsoft.Json;
using System;
using System.IO;

namespace Time2Rest.Config
{
    internal class T2rConfigManager
    {
        public static void WriteConfig(T2rConfig config)
        {
            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "config.json"))
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
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "config.json"))
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