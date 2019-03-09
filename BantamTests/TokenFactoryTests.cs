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
    }
}