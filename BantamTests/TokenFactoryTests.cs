using System.Collections.Generic;
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
            var token = factory.GetPunctuator("");
            Assert.IsTrue(token.IsEmpty);
        }
        public void Test11()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetPunctuator(null);
            Assert.IsTrue(token.IsEmpty);
        }
        public void Test111()
        {
            var factory = new TokenFactory(null);
            var token = factory.GetPunctuator(null);
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test2()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetName("");
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test21()
        {
            var punctuators = new Dictionary<string, TokenType>();
            var factory = new TokenFactory(punctuators);
            var token = factory.GetName(null);
            Assert.IsTrue(token.IsEmpty);
        }
        public void Test211()
        {
            var factory = new TokenFactory(null);
            var token = factory.GetName(null);
            Assert.IsTrue(token.IsEmpty);
        }
        [TestMethod]
        public void Test3()
        {
            var factory = new TokenFactory(null);
            foreach (var x in new[] { 
                "a", "b", "A", 
                "_" , "__", "X1"
                })
            {
                var token = factory.GetName(x);
                Assert.AreEqual(token.TokenType, TokenType.NAME, $"Tone:'{token}' is not {TokenType.NAME}");
                Assert.AreEqual(token.Value, x);
            }
        }
        [TestMethod]
        public void Test4()
        {
            var factory = new TokenFactory(null);            
            foreach (var x in new[] { 
                "?", "++", "-", "*", "&",
                "^", "%", "$", "#", 
                "@", // expression ? 
                "!", "~", ">", "<", ",",
                "", ";", "'", "\"", "`",
                "\"\"", "''", "@x", "[x]", "1X", // expression ?
                "1", "11", "0", "0.0" // Numbers ? 
                })
            {
                var token = factory.GetName(x);
                Assert.IsTrue(token.IsEmpty, $"Token:'{token}' Not Empty");
                Assert.AreEqual(token.TokenType, TokenType.NONE);
                Assert.AreEqual(token.Value, x);
            }
        }
    }
}