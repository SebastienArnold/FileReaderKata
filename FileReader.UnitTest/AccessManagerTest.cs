using FileReader.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileReader.UnitTest
{
    [TestClass]
    public class AccessManagerTest
    {
        [TestMethod]
        public void ShouldAuthorizeAdmin()
        {
            // Arrange
            string user = SimpleAccessManager.adminIdentity;
            var accessManager = new SimpleAccessManager();

            // Act
            var canAccess = accessManager.CanAccess("Resources\\FormattedXmlFile-userA.xml", user);

            // Assert
            Assert.IsTrue(canAccess);
        }

        [TestMethod]
        public void ShouldNotAuthorizeIfUserNotAllowed()
        {
            // Arrange
            string user = "unknownUser";
            var accessManager = new SimpleAccessManager();

            // Act
            var canAccess = accessManager.CanAccess("Resources\\FormattedXmlFile-userA.xml", user);

            // Assert
            Assert.IsFalse(canAccess);
        }

        [TestMethod]
        public void ShouldAuthorizeAllowedUser()
        {
            // Arrange
            string user = "userA";
            var accessManager = new SimpleAccessManager();

            // Act
            var canAccess = accessManager.CanAccess("Resources\\FormattedXmlFile-userA.xml", user);

            // Assert
            Assert.IsTrue(canAccess);
        }
    }
}
