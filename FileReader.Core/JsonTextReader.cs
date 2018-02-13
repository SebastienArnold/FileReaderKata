using FileReader.Interfaces;
using Newtonsoft.Json;

namespace FileReader.Core
{
    public class JsonTextReader : ITextReader
    {
        public string Read(string content)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(content);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
