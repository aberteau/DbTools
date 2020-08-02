using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DbTools.DbStructure.ExportConsoleApp
{
    public class JsonHelper
    {
        public static async Task WriteJsonAsync<T>(T obj, string filepath)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            await File.WriteAllTextAsync(filepath, json);
        }
    }
}
