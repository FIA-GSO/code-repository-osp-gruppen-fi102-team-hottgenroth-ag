using FluentAssertions;
using NUnit.Framework;

namespace Logisitcs.BLL.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void Test()
        {
            int abc = 12;
            abc.Should().Be(12);
        }
    }
}