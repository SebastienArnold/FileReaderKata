namespace FileReader.Interfaces
{
    public interface IReaderResult
    {
        bool AccessGranted { get; }
        string Content { get; }
    }
}
