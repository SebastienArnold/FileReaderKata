using System.IO;
using System.Text;
using System.Xml;
using FileReader.Core.Interfaces;

namespace FileReader.Core
{
    public class XmlTextReader : ITextReader
    {
        public string ReadFile(string path)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = new XmlTextWriter(memoryStream, Encoding.Unicode))
                {
                    var document = new XmlDocument();

                    document.Load(path);
                    xmlWriter.Formatting = Formatting.Indented;

                    document.WriteContentTo(xmlWriter);
                    xmlWriter.Flush();
                    memoryStream.Flush();

                    memoryStream.Position = 0;

                    var streamReader = new StreamReader(memoryStream);
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
