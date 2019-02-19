using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class FileCache
    {
        public FileCache()
        {

        }
        public static string CacheFileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Cache.txt";
        public static string LogFileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\Log\Logs.txt";
        public T GetValue<T>(string key, T defaultValue)
        {
            if (!File.Exists(CacheFileName)) {
                return defaultValue;
            }

            string[] cacheLines = File.ReadAllLines(CacheFileName);
            foreach (var item in cacheLines) {
                try {
                    KeyValuePair<string, T> a = JsonConvert.DeserializeObject<KeyValuePair<string, T>>(item);
                    if (a.Key == key) {
                        return a.Value;
                    }
                } catch (Newtonsoft.Json.JsonSerializationException ex) {

                }
            }
            return defaultValue;
        }
        public void SetValue<T>(string key, T value)
        {
            string json = "";
            KeyValuePair<string, T> keyValueToInsert = new KeyValuePair<string, T>(key,value);
            json = JsonConvert.SerializeObject(keyValueToInsert);

            List<string> cacheLines = new List<string>();
            if (File.Exists(CacheFileName)) {
                cacheLines = File.ReadAllLines(CacheFileName).ToList() ;
            }
            List<string> newCache = new List<string>();
            foreach (var item in cacheLines) {
                try {
                    KeyValuePair<string, T> b = JsonConvert.DeserializeObject<KeyValuePair<string, T>>(item);
                    if (b.Key == key) {
                        continue;
                    } else {
                        newCache.Add(item);
                    }
                } catch (Newtonsoft.Json.JsonSerializationException ex) {
                    newCache.Add(item);
                }
            }

            File.WriteAllLines(CacheFileName, newCache.ToArray());

            if (!File.Exists(CacheFileName)) {
                File.WriteAllText(CacheFileName, json + Environment.NewLine);
            } else {
                File.AppendAllText(CacheFileName, json  +Environment.NewLine);
            }
        }

        public static void WriteToLogFile(string Message)
        {
            try {
                string path = Path.GetDirectoryName(LogFileName);
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                string filepath = LogFileName;
                if (!File.Exists(filepath)) {
                    // Create a file to write to.   
                    using (StreamWriter sw = File.CreateText(filepath)) {
                        sw.WriteLine(DateTime.Now.ToString() + " | " + Message);
                    }
                } else {
                    using (StreamWriter sw = File.AppendText(filepath)) {
                        sw.WriteLine(DateTime.Now.ToString() + " | " + Message);
                    }
                }
            } catch (Exception ex) {

            }
        }


    }
}
