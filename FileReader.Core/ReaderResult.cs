using FileReader.Interfaces;

namespace FileReader.Core
{
    public class ReaderResult : IReaderResult
    {
        public ReaderResult(bool accessGranted, string content)
        {
            AccessGranted = accessGranted;
            Content = content;
        }

        public bool AccessGranted { get; private set; }
        public string Content { get; private set; }
    }
}
