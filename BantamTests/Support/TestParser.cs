using System;
using System.Collections.Generic;
using Bantam;
using SimpleParser;

namespace BantamTests.Support
{
    public class TestParser
    {
        private readonly TestParserConfig config;

        public TestParser(TestParserConfig config)
        {
            this.config = config;
        }

        public  string Parse(string source)
        {            
            var lexer = new Lexer(source);
            var parserMap = new FakeMap(config.PrefixParselets,config.InfixParselets);
            var parser = new BantamParser(lexer,parserMap);
            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.ToString();
            return actual;
        }

        public static TestParserFactory Factory =  new TestParserFactory();

        public  class TestParserFactory
       {
           public  TestParser CreateNew(
               IEnumerable<Tuple<TokenType, IPrefixParselet>> prefixes 
               , IEnumerable<Tuple<TokenType, InfixParselet>> infixes = null)
           {
               var config = TestParserConfig.Factory.CreateNew(prefixes, infixes);
               return new TestParser(config);
           }

            
       }
    }
}