using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

namespace DashCode.Models
{
    public static class FileReader
    {
        public static void SerializeToXML<T>(string path, T data)
        {
            var xmlSer = new XmlSerializer(data.GetType());
            using (var f = new StreamWriter(path))
            {
                xmlSer.Serialize(f, data);
            }
        }
        public static void SerializeToJson<T>(string path, T data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            using (var f = new StreamWriter(path))
            {
                f.WriteLine(jsonData);
            }
        }
        public static T DeserializeXML<T>(string path)
        {
            T data;
            var xmlSer = new XmlSerializer(typeof(T));
            using (var f = new StreamReader(path))
            {
                data = (T)xmlSer.Deserialize(f);
            }
            return data;
        }
        public static T DeserializeJson<T>(string path)
        {
            T data;
            using (var f = new StreamReader(path))
            {
                data = JsonSerializer.Deserialize<T>(f.ReadToEnd());
            }
            return data;
        }
    }
}
