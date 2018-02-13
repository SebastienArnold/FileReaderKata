namespace FileReader.Interfaces
{
    public interface IEncryptor
    {
        string Encrypt(string sourceContent);
        string Decrypt(string encryptedContent);
    }
}
