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
            string role = SimpleAccessManager.adminRole;
            var accessManager = new SimpleAccessManager();

            // Act
            var canAccess = accessManager.CanAccess("Resources\\FormattedXmlFile-roleA.xml", role);

            // Assert
            Assert.IsTrue(canAccess);
        }

        [TestMethod]
        public void ShouldNotAuthorizeIfUserNotAllowed()
        {
            // Arrange
            string unknownRole = "unknownRole";
            var accessManager = new SimpleAccessManager();

            // Act
            var canAccess = accessManager.CanAccess("Resources\\FormattedXmlFile-roleA.xml", unknownRole);

            // Assert
            Assert.IsFalse(canAccess);
        }

        [TestMethod]
        public void ShouldAuthorizeAllowedRole()
        {
            // Arrange
            string role = "roleA";
            var accessManager = new SimpleAccessManager();

            // Act
            var canAccess = accessManager.CanAccess("Resources\\FormattedXmlFile-roleA.xml", role);

            // Assert
            Assert.IsTrue(canAccess);
        }
    }
}
