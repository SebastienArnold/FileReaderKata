namespace FileReader.Interfaces
{
    public interface IInitializedReader
    {
        string ReadFile(string path);
    }

    public interface IFileReaderInitiating
    {
        IEncryptedTextReaderChaining UsingTextReader(ITextReader textReader);
    }

    public interface IEncryptedTextReaderChaining
    {
        IInitializedReader WithEncryptor(IEncryptor encryptor);
        IInitializedReader WithoutEncryption();
    }
}