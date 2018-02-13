using System;
using FileReader.Interfaces;

namespace FileReader.SimpleEncryption
{
    public class ReverseEncryptor : IEncryptor
    {
        public string Encrypt(string sourceContent)
        {
            return ReverseString(sourceContent);
        }

        public string Decrypt(string encryptedContent)
        {
            return ReverseString(encryptedContent);
        }

        private string ReverseString(string source)
        {
            var charArray = source.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
