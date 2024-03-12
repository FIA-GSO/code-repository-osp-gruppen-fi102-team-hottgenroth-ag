using FluentAssertions;
using Logisitcs.BLL;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

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