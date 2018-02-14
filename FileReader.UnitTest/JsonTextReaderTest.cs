using System.IO;
using FileReader.Core;
using FileReader.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class JsonTextReaderTest
    {
        [TestMethod]
        public void ShouldReadAndFormatJsonFile()
        {
            // Arrange
            ITextReader reader = new JsonTextReader();
            var expectedContent = File.ReadAllText("Resources\\FormattedJsonFile-roleA.json");

            // Act
            var content = reader.Read(File.ReadAllText("Resources\\JsonFile.json"));
            File.WriteAllText("jsonFile", content);
            // Assert
            Assert.AreEqual(expectedContent, content);
        }
    }
}
