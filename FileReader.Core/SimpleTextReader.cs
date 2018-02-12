using System.IO;
using FileReader.Core.Interfaces;

namespace FileReader.Core
{
    public class SimpleTextReader : ITextReader
    {
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
