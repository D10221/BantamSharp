using System.Collections.Generic;
using System.Linq;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class TokenFactoryTests
    {
        ///<summary>
        /// Returns Empty token
        ///</summary>
        [TestMethod]
        public void Test1()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetToken(TokenSource.From("", 0, 0));
            Assert.IsTrue(token.IsEmpty);
        }
        public void Test11()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetToken(null);
            Assert.IsTrue(token.IsEmpty);
        }
        public void Test111()
        {
            var factory = new TokenFactory(null);
            var token = factory.GetToken(null);
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test2()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetToken(TokenSource.From("", 0, 0));
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test21()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetToken(null);
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test211()
        {
            var factory = TokenFactory.From(null);
            var token = factory.GetToken(null);
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test3()
        {
            var factory = TokenFactory.From(null);
            foreach (var x in new[] {
                "a", "b", "A",
                "_" , "__", "X1"
                }.Select((x, i) => TokenSource.From(x, 0, i)))
            {
                var token = factory.GetToken(x);
                Assert.AreEqual(token.TokenType, TokenType.NAME, $"Tone:'{token}' is not {TokenType.NAME}");
                Assert.AreEqual(token.Value, x);
            }
        }
        [TestMethod]
        public void TestEmptyToken()
        {
            var factory = TokenFactory.From(null);
            foreach (var x in new[] {
                "?", "++", "-", "*", "&",
                "^", "%", "$", "#",
                "@", // expression ? 
                "!", "~", ">", "<", ",",
                "", ";", "'", "\"", "`",
                 "@x", "[x]", "1X", // expression ?                                 
                }.Select((x, i) => TokenSource.From(x, 0, i)))
            {
                var token = factory.GetToken(x);
                Assert.IsTrue(token.IsEmpty, $"Token:'{token.TokenType}:{token}' Not Empty");
                Assert.AreEqual(token.TokenType, TokenType.NONE);
                Assert.AreEqual(token.Value, x);
            }
        }
        [TestMethod]
        public void TestNumbers()
        {
            var factory = TokenFactory.From(null);
            foreach (var x in new[] { "1", "11", "0", "0.0" }.Select((x, i) => TokenSource.From(x, 0, i)))
            {
                var token = factory.GetToken(x);
                Assert.IsTrue(!token.IsEmpty, $"Token:'{token.TokenType}:{token}' is Empty");
                Assert.AreEqual(token.TokenType, TokenType.NUMBER);
                Assert.AreEqual(token.Value, x);
            }

        }
        [TestMethod]
        public void TestLiterals()
        {
            var factory = TokenFactory.From(null);
            foreach (var x in new[]{
                "\"\"", "''", // literal
            }.Select((x, i) => TokenSource.From(x, 0, i)))
            {
                var token = factory.GetToken(x);
                Assert.IsTrue(!token.IsEmpty, $"Token:'{token.TokenType}:{token}' is Empty");
                Assert.AreEqual(token.TokenType, TokenType.LITERAL);
                Assert.AreEqual(token.Value, x);
            }

        }
    }
}