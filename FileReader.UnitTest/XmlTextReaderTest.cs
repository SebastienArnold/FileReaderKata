﻿using System.IO;
using FileReader.Core;
using FileReader.Interfaces;
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
            var content = reader.Read(File.ReadAllText("Resources\\FormattedXmlFile.xml"));

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
            var content = reader.Read(File.ReadAllText(("Resources\\InlineXmlFile.xml")));

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
            reader.Read(File.ReadAllText("Resources\\NotExistingFile.txt"));

            // Assert exception
        }

        [TestMethod]
        [ExpectedException(typeof(System.Xml.XmlException))]
        public void ShouldThrowXmlExceptionWhenXmlIsNotValid()
        {
            // Arrange
            ITextReader reader = new XmlTextReader();

            // Act
            reader.Read(File.ReadAllText("Resources\\SimpleTextFile.txt"));

            // Assert exception
        }
    }
}
