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
            var expected = File.ReadAllText("SimpleTextFile.txt");

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
