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
        [DataRow("SimpleTextReader", "TextFile-roleA.txt", "RevertedTextFile.txt")]
        [DataRow("XmlTextReader", "FormattedXmlFile-roleA.xml", "RevertedXmlFile.xml")]
        [DataRow("JsonTextReader", "FormattedJsonFile-roleA.json", "RevertedJsonFile.json")]
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

            File.WriteAllText("text", readerResult.Content);

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedContent, readerResult.Content);
        }

        [TestMethod]
        [DataRow("XmlTextReader", "FormattedXmlFile-roleA.xml")]
        [DataRow("SimpleTextReader", "TextFile-roleA.txt")]
        [DataRow("JsonTextReader", "FormattedJsonFile-roleA.json")]
        public void ShouldBeNotGrantedToReadIfUseSecurityAndNotAuthorizedRole
            (string textReaderName, string fileNameWithContentToRead)
        {
            // Arrange
            var role = "roleB";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), role)
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsFalse(readerResult.AccessGranted);
            Assert.AreEqual(string.Empty, readerResult.Content);
        }

        [TestMethod]
        [DataRow("XmlTextReader", "FormattedXmlFile-roleA.xml")]
        [DataRow("SimpleTextReader", "TextFile-roleA.txt")]
        [DataRow("JsonTextReader", "FormattedJsonFile-roleA.json")]
        public void ShouldBeGrantedToReadIfUseSecurityAndAuthorizedUser
            (string textReaderName, string fileNameWithContentToRead)
        {
            // Arrange
            var expectedContent = File.ReadAllText($"Resources\\{fileNameWithContentToRead}");
            var allowedRole = "roleA";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), allowedRole)
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedContent, readerResult.Content);
        }

        [TestMethod]
        [DataRow("XmlTextReader", "FormattedXmlFile-roleA.xml")]
        [DataRow("SimpleTextReader", "TextFile-roleA.txt")]
        [DataRow("JsonTextReader", "FormattedJsonFile-roleA.json")]
        public void ShouldBeGrantedToReadIfUseSecurityAndAdminUser(string textReaderName, string fileNameWithContentToRead)
        {
            // Arrange
            var expectedContent = File.ReadAllText($"Resources\\{fileNameWithContentToRead}");
            var adminRole = SimpleAccessManager.adminRole;

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(_readers[textReaderName])
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), adminRole)
                .ReadFile($"Resources\\{fileNameWithContentToRead}");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedContent, readerResult.Content);
        }
    }
}