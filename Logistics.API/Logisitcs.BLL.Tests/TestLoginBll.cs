using FluentAssertions;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using Logisitcs.DAL;
using NUnit.Framework;
using System.Linq;

namespace Logisitcs.BLL.Tests
{
    [TestFixture]
    public class TestLoginBll
    {
        [Test]
        public void Test_LoginBll()
        {
            ILoginBll loginBll = new LoginBll();
            //Setup the test data
            ILoginData loginData = new LoginData() { Password = "12345", UserEmail = "feser@schule.koeln" };
            //Test the Register and check if the user is registered with the correct role
            IUserData userData = loginBll.Register(loginData).Result;
            userData.UserEmail.Should().Be("feser@schule.koeln");
            userData.Role.Should().Be("User");
            loginData = new LoginData() { Password = "12345", UserEmail = "feser@schule.koeln" };
            //Test the Login and check if the user is logged in with the correct role
            userData = loginBll.Login(loginData).Result;
            userData.Role.Should().Be("User");
            userData.Role = "Admin";
            //Test the UpdateRole and check if the role is updated
            var updateResult = loginBll.UpdateRole(userData).Result;
            updateResult.Should().BeTrue();
            //Test the GetAllUser and check if the user is in the list
            var users = loginBll.GetAllUser().Result;
            var user = users.Single(x => x.UserEmail == "feser@schule.koeln");
            //Check if the user is in the list
            user.UserEmail.Should().Be("feser@schule.koeln");
            userData.Role.Should().Be("Admin");
            //Delete the test User
            DbCommandsUser.DeleteUser(user.UserId.ToString());
        }
    }
}