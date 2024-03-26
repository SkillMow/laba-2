using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class Serializer
{
    public static async Task SerializeObjectAsync<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            await Task.Run(() => xmlSerializer.Serialize(fs, obj));
        }
    }

    public static async Task<T> DeserializeObjectAsync<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return await Task.Run(() => (T)xmlSerializer.Deserialize(fs));
        }
    }
}
