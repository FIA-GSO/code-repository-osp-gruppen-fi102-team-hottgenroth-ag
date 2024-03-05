using Xunit;
using Moq;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Models;
using Logisitcs.DAL;
using Logisitcs.DAL.Models;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Tests
{
    public class LoginBllTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsUserData()
        {
            // Arrange
            var loginData = new Mock<ILoginData>();
            loginData.SetupGet(x => x.UserEmail).Returns("test@example.com");
            loginData.SetupGet(x => x.Password).Returns("password");

            var mockDBCommands = new Mock<IDBCommands>();
            mockDBCommands.Setup(x => x.GetUserByMail(It.IsAny<string>())).Returns(new User
            {
                UserId = "123",
                UserEmail = "test@example.com",
                UserPassword = PasswordHashHelper.Hash("password"),
                UserRoleId = 1
            });

            var loginBll = new LoginBll(mockDBCommands.Object);

            // Act
            var result = await loginBll.Login(loginData.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123", result.UserId.ToString());
            Assert.Equal("test@example.com", result.UserEmail);
            Assert.Equal("Admin", result.Role);
        }

        [Fact]
        public async Task Register_NewUser_SuccessfullyRegistered()
        {
            // Arrange
            var loginData = new Mock<ILoginData>();
            loginData.SetupGet(x => x.UserEmail).Returns("newuser@example.com");
            loginData.SetupGet(x => x.Password).Returns("password");

            var mockDBCommands = new Mock<IDBCommands>();
            mockDBCommands.Setup(x => x.GetUserByMail(It.IsAny<string>())).Returns((User)null);

            var loginBll = new LoginBll(mockDBCommands.Object);

            // Act
            var result = await loginBll.Register(loginData.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("newuser@example.com", result.UserEmail);
        }
    }
}