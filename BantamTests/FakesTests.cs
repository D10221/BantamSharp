using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class FakesTests
    {
        [TestMethod]
        public void FakeLexerTest()
        {
            var lexer = new FakeLexer("a");
            var t = lexer.Next();
            Assert.AreEqual("a", t.Text);
        }

        [TestMethod]
        public void FakeLexerTest2()
        {
            var lexer = new FakeLexer("ab");
            Assert.AreEqual("a", lexer.Next().Text);
            Assert.AreEqual("b", lexer.Next().Text);
        }

        [TestMethod]
        public void FakeLexerTest3()
        {
            var lexer = new FakeLexer("");
            //WARNING ! , null not ok 
            Assert.AreEqual(null, lexer.Next().Text);
        }

        [TestMethod]
        public void FakeBuilderTest()
        {
            var fake = new FakeExpression("x");
            var builder = new Builder();
            fake.Print(builder);
            Assert.AreEqual("x", builder.Build());
        }
    }
}
