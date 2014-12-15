using System;
using System.Collections.Generic;
using Bantam;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet<SimpleParser.TokenType>> ;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet<SimpleParser.TokenType>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleParser.TokenType>;
using ParserMap = SimpleParser.ParserMap<SimpleParser.TokenType>;
using IParserMap = SimpleParser.IParserMap<SimpleParser.TokenType>;
using Parser = SimpleParser.Parser<SimpleParser.TokenType,char>;


namespace BantamTests.Support
{
    public class TestParser
    {
        private readonly ParserConfig _config;

        public TestParser(ParserConfig config, FakeBuilder builder)
        {
            _config = config;
            _builder = builder;           
        }

        public  string Parse(string source)
        {            
            var lexer = new Lexer(source, new TokenConfig());
            var parserMap = new ParserMap(_config.PrefixParselets,_config.InfixParselets);
            var parser = new Parser(lexer,parserMap);
            var result = parser.ParseExpression();
           
            result.Print(_builder);
            var actual = _builder.Build();
            return actual;
        }

        public static readonly TestParserFactory Factory =  new TestParserFactory();
        private readonly FakeBuilder _builder;

        public  class TestParserFactory
       {
           public  TestParser CreateNew(IEnumerable<Prefix> prefixes , IEnumerable<Infix> infixes = null)
           {
               var config = ParserConfig.Factory.CreateNew(prefixes, infixes);
               return new TestParser(config, new FakeBuilder());
           }

            
       }
    }
}