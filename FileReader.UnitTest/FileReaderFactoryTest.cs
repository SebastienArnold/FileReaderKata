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
        public void ShouldBeNotGrantedIfUseSecurityAndNotAuthorizedUser()
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
        public void ShouldBeGrantedIfUseSecurityAndAuthorizedUser()
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
        public void ShouldBeGrantedIfUseSecurityAndAdminUser()
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
