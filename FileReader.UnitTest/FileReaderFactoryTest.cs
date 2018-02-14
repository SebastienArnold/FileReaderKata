using System.IO;
using FileReader.Core;
using FileReader.SimpleEncryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FileReader.Interfaces;

namespace FileReader.UnitTest
{
    [TestClass]
    public class FileReaderFactoryTest
    {
        private Dictionary<string, ITextReader> _readers = new Dictionary<string, ITextReader>();

        [TestInitialize]
        public void Setup()
        {
            _readers.Add("SimpleTextReader", new SimpleTextReader());
            _readers.Add("XmlTextReader", new XmlTextReader());
            _readers.Add("JsonTextReader", new JsonTextReader());
        }   

        [TestMethod]
        [DataRow("SimpleTextReader", "TextFile-userA.txt", "RevertedTextFile.txt")]
        [DataRow("XmlTextReader", "FormattedXmlFile-userA.xml", "RevertedXmlFile.xml")]
        [DataRow("JsonTextReader", "FormattedJsonFile-userA.json", "RevertedJsonFile.json")]
        public void ShouldBeAbleToReadReversedFile(string textReaderName, string fileNameWithExpected, string fileNameWithContentToRead)
        {
            // Arrange
            var expectedContent = File.ReadAllText($"Resources\\{fileNameWithExpected}");

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithEncryptor(new ReverseEncryptor())
                .WithoutSecurity()
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedContent, readerResult.Content);
        }

        [TestMethod]
        [DataRow("XmlTextReader", "FormattedXmlFile-userA.xml")]
        [DataRow("SimpleTextReader", "TextFile-userA.txt")]
        [DataRow("JsonTextReader", "FormattedJsonFile-userA.json")]
        public void ShouldBeNotGrantedToReadXmlIfUseSecurityAndNotAuthorizedUser
            (string textReaderName, string fileNameWithContentToRead)
        {
            // Arrange
            var user = "userB";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), user)
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsFalse(readerResult.AccessGranted);
            Assert.AreEqual(string.Empty, readerResult.Content);
        }

        [TestMethod]
        [DataRow("XmlTextReader", "FormattedXmlFile-userA.xml")]
        [DataRow("SimpleTextReader", "TextFile-userA.txt")]
        [DataRow("JsonTextReader", "FormattedJsonFile-userA.json")]
        public void ShouldBeGrantedToReadIfUseSecurityAndAuthorizedUser
            (string textReaderName, string fileNameWithContentToRead)
        {
            // Arrange
            var expectedContent = File.ReadAllText($"Resources\\{fileNameWithContentToRead}");
            var allowedUser = "userA";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), allowedUser)
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedContent, readerResult.Content);
        }

        [TestMethod]
        [DataRow("XmlTextReader", "FormattedXmlFile-userA.xml")]
        [DataRow("SimpleTextReader", "TextFile-userA.txt")]
        [DataRow("JsonTextReader", "FormattedJsonFile-userA.json")]
        public void ShouldBeGrantedToReadIfUseSecurityAndAdminUser(string textReaderName, string fileNameWithContentToRead)
        {
            // Arrange
            var expectedContent = File.ReadAllText($"Resources\\{fileNameWithContentToRead}");
            var adminUser = SimpleAccessManager.adminIdentity;

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), adminUser)
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedContent, readerResult.Content);
        }
    }
}
