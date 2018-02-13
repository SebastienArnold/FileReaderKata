namespace FileReader.Interfaces.FluentReaderFactory
{
    public interface IEncryptedTextReaderChaining
    {
        IInitializedReader WithEncryptor(IEncryptor encryptor);
        IInitializedReader WithoutEncryption();
    }
}