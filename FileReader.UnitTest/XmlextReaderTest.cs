using System.IO;
using FileReader.Core;
using FileReader.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class XmlTextReaderTest
    {
        [TestMethod]
        public void ShouldReadAnXmlFile()
        {
            // Arrange
            ITextReader reader = new XmlTextReader();
            var expected = File.ReadAllText("Resources\\FormattedXmlFile.xml");
            
            // Act
            var content = reader.ReadFile("Resources\\FormattedXmlFile.xml");

            // Assert
            Assert.AreEqual(expected, content);
        }

        [TestMethod]
        public void ShouldFormatXmlFile()
        {
            // Arrange
            ITextReader reader = new XmlTextReader();
            var expected = File.ReadAllText("Resources\\FormattedXmlFile.xml");

            // Act
            var content = reader.ReadFile("Resources\\InlineXmlFile.xml");

            // Assert
            Assert.AreEqual(expected, content);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowFileNotFoundExceptionWhenFileDoNotExist()
        {
            // Arrange
            ITextReader reader = new XmlTextReader();

            // Act
            reader.ReadFile("Resources\\NotExistingFile.txt");

            // Assert exception
        }

        [TestMethod]
        [ExpectedException(typeof(System.Xml.XmlException))]
        public void ShouldThrowXmlExceptionWhenXmlIsNotValid()
        {
            // Arrange
            ITextReader reader = new XmlTextReader();

            // Act
            reader.ReadFile("Resources\\SimpleTextFile.txt");

            // Assert exception
        }
    }
}
