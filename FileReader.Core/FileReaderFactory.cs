using System;
using FileReader.Interfaces;
using FileReader.Interfaces.FluentReaderFactory;
using System.IO;

namespace FileReader.Core
{
    public class FileReaderFactory : IInitializedReader, IFileReaderInitiating, IEncryptedTextReaderChaining, IInitializedAndSecurizedReader
    {
        private ITextReader _textReader;
        private IEncryptor _encryptor;
        private IAccessManager _accessManager;
        private string _identity;

        /// <summary>
        /// Fluent step 1 - Initialized reader
        /// </summary>
        /// <returns></returns>
        public static IFileReaderInitiating InitializeReader()
        {
            return new FileReaderFactory();
        }

        /// <summary>
        /// Fluent step 2 - Assign a text reader
        /// </summary>
        /// <param name="textReader"></param>
        /// <returns></returns>
        IEncryptedTextReaderChaining IFileReaderInitiating.UsingTextReader(ITextReader textReader)
        {
            _textReader = textReader ?? throw new NullReferenceException("TextReader is null");
            return this;
        }

        /// <summary>
        /// Fluent step 3 - Choose to use an encryptor
        /// </summary>
        /// <param name="encryptor"></param>
        /// <returns></returns>
        IInitializedReader IEncryptedTextReaderChaining.WithEncryptor(IEncryptor encryptor)
        {
            _encryptor = encryptor ?? throw new NullReferenceException("Encryptor is null");
            return this;
        }

        /// <summary>
        /// Fluent step 3' - Choose to not use encryptor
        /// </summary>
        /// <returns></returns>
        IInitializedReader IEncryptedTextReaderChaining.WithoutEncryption()
        {
            return this;
        }

        /// <summary>
        /// Fluent step 4 - Choose to use a security manager
        /// </summary>
        /// <param name="accessManager"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        IInitializedAndSecurizedReader IInitializedReader.WithSecurityAsUser(IAccessManager accessManager, string identity)
        {
            _accessManager = accessManager;
            _identity = identity;

            return this;
        }

        /// <summary>
        /// Fluent step 4' - Choose to not use a security manager
        /// </summary>
        /// <returns></returns>
        IInitializedAndSecurizedReader IInitializedReader.WithoutSecurity()
        {
            return this;
        }

        /// <summary>
        /// Fluent step final - Use configured reader to read file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IReaderResult IInitializedAndSecurizedReader.ReadFile(string path)
        {
            var accessGranted = true;
            if (_accessManager != null)
            {
                accessGranted = _accessManager.CanAccess(path, _identity);
            }

            if (!accessGranted)
            {
                return new ReaderResult(false, string.Empty);
            }

            var fileContent = File.ReadAllText(path);

            if (_encryptor != null)
            {
                fileContent = _encryptor.Decrypt(fileContent);
            }

            File.WriteAllText("jsonreversed", fileContent);

            var contentRead = _textReader.Read(fileContent);
            return new ReaderResult(true, contentRead);
        }
    }
}