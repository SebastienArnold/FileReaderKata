using System.IO;
using FileReader.Core;
using FileReader.SimpleEncryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class EncryptedReaderTest
    {
        [TestMethod]
        public void ShouldBeAbleToReadReversedFile()
        {
            // Arrange
            var simpleText = File.ReadAllText("Resources\\SimpleTextFile.txt");

            // Act
            var simpleReversedText = FileReaderFactory.InitializeReader()
                .UsingTextReader(new SimpleTextReader())
                .WithEncryptor(new ReverseEncryptor())
                .ReadFile("Resources\\SimpleReversedTextFile.txt");

            // Assert
            Assert.AreEqual(simpleText, simpleReversedText);
        }
    }
}
