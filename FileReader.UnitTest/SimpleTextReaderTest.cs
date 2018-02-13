using System.IO;
using FileReader.Core;
using FileReader.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class SimpleTextReaderTest
    {
        [TestMethod]
        public void ShouldReadASimpleFile()
        {
            // Arrange
            ITextReader reader = new SimpleTextReader();
            var expected = "My line 1\r\n\r\nMy line 3 (2 is empty)";

            // Act
            var content = reader.Read(File.ReadAllText("Resources\\SimpleTextFile.txt"));

            // Assert
            Assert.AreEqual(expected, content);
        }
    }
}
