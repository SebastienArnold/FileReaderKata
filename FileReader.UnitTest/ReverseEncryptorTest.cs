using FileReader.SimpleEncryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class ReverseEncryptorTest
    {
        [TestMethod]
        public void ShouldDecryptEncryptedContent()
        {
            // Arrange
            var testString = "My simple test string";
            var encryptor = new ReverseEncryptor();

            // Act
            var encrypted = encryptor.Encrypt(testString);
            var decrypted = encryptor.Decrypt(encrypted);

            // Assert
            Assert.AreNotEqual(testString, encrypted);
            Assert.AreEqual(testString, decrypted);
        }
    }
}
