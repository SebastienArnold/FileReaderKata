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
            var expected = File.ReadAllText("Resources\\TextFile-roleA.txt");

            // Act
            var content = reader.Read(File.ReadAllText("Resources\\TextFile-roleA.txt"));

            // Assert
            Assert.AreEqual(expected, content);
        }
    }
}
