using System;
using FileReader.Interfaces;

namespace FileReader.Core
{
    public class FileReaderFactory : IInitializedReader, IFileReaderInitiating, IEncryptedTextReaderChaining
    {
        private ITextReader _textReader;
        private IEncryptor _encryptor;
        
        public static IFileReaderInitiating InitializeReader()
        {
            return new FileReaderFactory();
        }

        IEncryptedTextReaderChaining IFileReaderInitiating.UsingTextReader(ITextReader textReader)
        {
            _textReader = textReader ?? throw new NullReferenceException("TextReader is null");
            return this;
        }

        IInitializedReader IEncryptedTextReaderChaining.WithEncryptor(IEncryptor encryptor)
        {
            _encryptor = encryptor ?? throw new NullReferenceException("Encryptor is null");
            return this;
        }

        IInitializedReader IEncryptedTextReaderChaining.WithoutEncryption()
        {
            return this;
        }

        string IInitializedReader.ReadFile(string path)
        {
            var content = _textReader.ReadFile(path);

            if(_encryptor != null)
            {
                return _encryptor.Decrypt(content);
            }

            return content;
        }
    }
}