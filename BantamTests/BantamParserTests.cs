using Bantam;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;

namespace BantamTests
{
    [TestClass] // ? 
    public class BantamParserFunctionCallTests
    {
        [TestMethod]
        public void FunctionCallTest()
        {
            var s =Parse("a()");
            Assert.AreEqual("a()",s);
        }
        
        [TestMethod]
        public void FunctionCallTest1()
        {
            const string expression = "a(b)";
            var s =Parse(expression);
            Assert.AreEqual(expression,s);
        }
        
        [TestMethod]
        public void FunctionCallTest2()
        {
            const string expression = "a(b, c)";
            var s =Parse(expression);
            Assert.AreEqual(expression,s);
        }
        
       /* [TestMethod]
        public void FunctionCallTest4()
        {
            const string expression = "a(b)(c)";
            var s =Parse(expression);
            Assert.AreEqual(expression,s);
        }*/
        
        [TestMethod]
        public void FunctionCallTest5()
        {
            const string expression = "(a(b) + c(d))";
            var s =Parse(expression);
            Assert.AreEqual(expression,s);
        }

        [TestMethod]
        public void FunctionCallTest51()
        {
            const string expression = "a(b) + c(d)";
            var s =Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(a(b) + c(d))", s);
        }
        
        [TestMethod]
        public void FunctionCallTest6()
        {
            const string expression = "a(b ? c : d, e + f)";
            var s =Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("a((b ? c : d), (e + f))", s);
        }

        public static string Parse(string source)
        {
            var tokenConfig = new TokenConfig();
            var lexer = new Lexer(source, tokenConfig);
            var parser = new Parser(lexer,new BantamMap(tokenConfig));

            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.Build();
            return actual;
        }
    }
}
