using System.IO;
using System.Text;
using System.Xml;
using FileReader.Interfaces;

namespace FileReader.Core
{
    public class XmlTextReader : ITextReader
    {
        public string Read(string content)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = new XmlTextWriter(memoryStream, Encoding.Unicode))
                {
                    var document = new XmlDocument();

                    document.LoadXml(content);
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
