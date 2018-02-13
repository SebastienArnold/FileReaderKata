

namespace FileReader.Interfaces.FluentReaderFactory
{
    public interface IInitializedAndSecurizedReader
    {
        IReaderResult ReadFile(string path);
    }
}