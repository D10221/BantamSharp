﻿using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ThrowsParseException()
        {
            Assert.ThrowsException<ParseException>(() =>
            {
                var parse = ParserFactory.Create();
                // Parser doesnt find 'NEXT' 'B' ...'C'
                // Parser can't parse NAME NAME ....                                                
                var e = parse(@"A B C");                
            });
        }
         [TestMethod]
        public void ThrowsParseException2()
        {
            Assert.ThrowsException<ParseException>(() =>
            {
                var parse = ParserFactory.Create();                                                             
                var e = parse(@"1 B 1");                
            });
        }
    }
}