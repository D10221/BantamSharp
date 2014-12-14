using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class FakesTests
    {
        [TestMethod]
        public void FakesTest()
        {
            var fake = new FakeExpression("x");
            var builder = new FakeBuilder();
            fake.Print(builder);
            Assert.AreEqual("x", builder.ToString());
        }
    }
}
