using System.IO;
using FileReader.Core;
using FileReader.Core.Interfaces;
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
            ISimpleTextReader reader = new SimpleTextReader();
            var expected = "My line 1\r\n\r\nMy line 3 (2 is empty)";

            // Act
            var content = reader.ReadFile("SimpleTextFile.txt");

            // Assert
            Assert.AreEqual(expected, content);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowFileNotFoundExceptionWhenFileDoNotExist()
        {
            // Arrange
            ISimpleTextReader reader = new SimpleTextReader();

            // Act
            reader.ReadFile("NotExistingFile.txt");

            // Assert exception
        }
    }
}
