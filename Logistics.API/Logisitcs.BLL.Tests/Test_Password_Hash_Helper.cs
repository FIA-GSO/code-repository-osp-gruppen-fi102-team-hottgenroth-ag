using FluentAssertions;
using Logisitcs.BLL.Helper;
using NUnit.Framework;
using System;

namespace Logisitcs.BLL.Tests
{
    [TestFixture]
    public class Test_Password_Hash_Helper
    {
        [Test]
        public void Test_Hash()
        {
            string password = "password";
            // Hash the password
            string hash = PasswordHashHelper.Hash(password);
            // Check if hash is supported
            bool isSupported = PasswordHashHelper.IsHashSupported(hash);
            isSupported.Should().BeTrue();
            // Check if hash is correct
            bool verify = PasswordHashHelper.Verify(password, hash);
            verify.Should().BeTrue();
        }

        [Test]
        public void Test_Hast_Not_Supported()
        {
            string hash = "hash";
            // Hash should not be supported
            bool isSupported = PasswordHashHelper.IsHashSupported(hash);
            isSupported.Should().BeFalse();
        }

        [Test]
        public void Test_Verify_Should_Be_False()
        {
            string password = "password";
            string hash = PasswordHashHelper.Hash(password);
            // Verify with wrong password should be false
            bool verify = PasswordHashHelper.Verify("wrongPassword", hash);
            verify.Should().BeFalse();
        }
    }
}