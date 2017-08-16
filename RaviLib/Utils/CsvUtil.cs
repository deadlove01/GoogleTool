using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaviLib.Utils
{
    public class CsvUtil
    {
        public static void WriteToCSV<T>(string path, bool append, List<T> modelList)
        {
            using (StreamWriter stream = new StreamWriter(path, append))
            {
                using (CsvHelper.CsvWriter cw = new CsvHelper.CsvWriter(stream))
                {
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        cw.WriteRecord(modelList[i]);
                        cw.NextRecord();
                    }

                }
            }
        }

        public static void WriteObjectsToCSV<T>(List<T> objList, string savePath, string fileName)
        {
            // check path
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            Type type = typeof(T);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name);

            using (StreamWriter sw = new StreamWriter(savePath + fileName))
            {
                sw.WriteLine(String.Join(",", props.Select(p => p.Name)));

                foreach (var item in objList)
                {
                    sw.WriteLine(String.Join(",", props.Select(p => p.GetValue(item, null))));
                }
            }
        
        }

        public static List<T> ReadObjectsFromCSV<T>(string savePath, string fileName) where T : new()
        {
            List<T> result = new List<T>();
            Type type = typeof(T);
            //var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //using (StreamReader sr = new StreamReader(savePath + fileName))
            {
                string[] lines = File.ReadAllLines(savePath + fileName);
                if (lines != null && lines.Length > 0)
                {

                    foreach (var line in lines)
                    {
                        T t = new T();

                        if(!string.IsNullOrEmpty(line))
                        {
                            string[] data = line.Split(',');
                            int i = 0;

                            foreach (var prop in props)
                            {
                                //if(prop.Name != "Id")
                                prop.SetValue(t, data[i++], null);
                            }

                            result.Add(t);
                        }
                       
                    }

                }
            }

            return result;
        }
    }
}
