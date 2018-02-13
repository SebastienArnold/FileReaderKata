

namespace FileReader.Interfaces.FluentReaderFactory
{
    public interface IFileReaderInitiating
    {
        IEncryptedTextReaderChaining UsingTextReader(ITextReader textReader);
    }
}