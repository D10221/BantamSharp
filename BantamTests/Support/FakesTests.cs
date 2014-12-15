using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet<SimpleParser.TokenType>>;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet<SimpleParser.TokenType>>;

namespace BantamTests.Support
{
    [TestClass]
    public class FakesTests
    {
        [TestMethod]
        public void FakeLexerTest()
        {
            var lexer = new FakeLexer("a");
            var t = lexer.Next();
            Assert.AreEqual("a", t.GetText());
        }

        [TestMethod]
        public void FakeLexerTest2()
        {
            var lexer = new FakeLexer("ab");
            Assert.AreEqual("a", lexer.Next().GetText());
            Assert.AreEqual("b", lexer.Next().GetText());
        }

        [TestMethod]
        public void FakeLexerTest3()
        {
            var lexer = new FakeLexer("");
            //WARNING ! , null not ok 
            Assert.AreEqual(null, lexer.Next().GetText());          
        }

        [TestMethod]
        public void FakeBuilderTest()
        {
            var fake = new FakeExpression("x");
            var builder = new FakeBuilder();
            fake.Print(builder);
            Assert.AreEqual("x", builder.Build());
        }
        
    }

    
}
