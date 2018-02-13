using System.IO;
using FileReader.Core;
using FileReader.SimpleEncryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class FileReaderFactoryTest
    {
        [TestMethod]
        public void ShouldBeAbleToReadReversedFile()
        {
            // Arrange
            var simpleText = File.ReadAllText("Resources\\SimpleTextFile.txt");

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new SimpleTextReader())
                .WithEncryptor(new ReverseEncryptor())
                .WithoutSecurity()
                .ReadFile("Resources\\SimpleReversedTextFile.txt");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(simpleText, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeAbleToReadReversedXml()
        {
            // Arrange
            var expectedXmlContent = File.ReadAllText("Resources\\FormattedXmlFile.xml");

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new XmlTextReader())
                .WithEncryptor(new ReverseEncryptor())
                .WithoutSecurity()
                .ReadFile("Resources\\FormattedXmlFileReversed.xml");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(expectedXmlContent, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeNotGrantedToReadXmlIfUseSecurityAndNotAuthorizedUser()
        {
            // Arrange
            var user = "userB";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new XmlTextReader())
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), user)
                .ReadFile("Resources\\FormattedXmlFile-userA.xml");

            // Assert
            Assert.IsFalse(readerResult.AccessGranted);
            Assert.AreEqual(string.Empty, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeGrantedToReadXmlIfUseSecurityAndAuthorizedUser()
        {
            // Arrange
            var xmlContent = File.ReadAllText("Resources\\FormattedXmlFile-userA.xml");
            var allowedUser = "userA";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new XmlTextReader())
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), allowedUser)
                .ReadFile("Resources\\FormattedXmlFile-userA.xml");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(xmlContent, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeGrantedToReadXmlIfUseSecurityAndAdminUser()
        {
            // Arrange
            var xmlContent = File.ReadAllText("Resources\\FormattedXmlFile-userA.xml");
            var adminUser = SimpleAccessManager.adminIdentity;

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new XmlTextReader())
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), adminUser)
                .ReadFile("Resources\\FormattedXmlFile-userA.xml");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(xmlContent, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeNotGrantedToReadTextIfUseSecurityAndNotAuthorizedUser()
        {
            // Arrange
            var user = "userB";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new SimpleTextReader())
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), user)
                .ReadFile("Resources\\SimpleTextFile-userA.txt");

            // Assert
            Assert.IsFalse(readerResult.AccessGranted);
            Assert.AreEqual(string.Empty, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeGrantedToReadTextFileIfUseSecurityAndAuthorizedUser()
        {
            // Arrange
            var simpleText = File.ReadAllText("Resources\\SimpleTextFile-userA.txt");
            var allowedUser = "userA";

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new SimpleTextReader())
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), allowedUser)
                .ReadFile("Resources\\SimpleTextFile-userA.txt");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(simpleText, readerResult.Content);
        }

        [TestMethod]
        public void ShouldBeGrantedToReadTextFileIfUseSecurityAndAdminUser()
        {
            // Arrange
            var simpleText = File.ReadAllText("Resources\\SimpleTextFile-userA.txt");
            var allowedUser = SimpleAccessManager.adminIdentity;

            // Act
            var readerResult = FileReaderFactory.InitializeReader()
                .UsingTextReader(new SimpleTextReader())
                .WithoutEncryption()
                .WithSecurityAsUser(new SimpleAccessManager(), allowedUser)
                .ReadFile("Resources\\SimpleTextFile-userA.txt");

            // Assert
            Assert.IsTrue(readerResult.AccessGranted);
            Assert.AreEqual(simpleText, readerResult.Content);
        }
    }
}
