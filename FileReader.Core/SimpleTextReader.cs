using FileReader.Interfaces;

namespace FileReader.Core
{
    public class SimpleTextReader : ITextReader
    {
        public string Read(string content)
        {
            return content;
        }
    }
}
