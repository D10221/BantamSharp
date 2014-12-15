using System;
using System.Collections.Generic;
using Bantam;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet> ;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet>;

namespace BantamTests.Support
{
    public class TestParser
    {
        private readonly TestParserConfig _config;

        public TestParser(TestParserConfig config, FakeBuilder builder,
            Func<TestParserConfig,IParserMap> parserMapFactory=null)
        {
            _config = config;
            _builder = builder;
            _parserMapFactory = parserMapFactory ?? (z => new FakeMap(_config.PrefixParselets, _config.InfixParselets));
        }

        public  string Parse(string source)
        {            
            var lexer = new Lexer(source);
            var parserMap = new FakeMap(_config.PrefixParselets,_config.InfixParselets);
            var parser = new BantamParser(lexer,parserMap);
            var result = parser.ParseExpression();
           
            result.Print(_builder);
            var actual = _builder.Build();
            return actual;
        }

        public static readonly TestParserFactory Factory =  new TestParserFactory();
        private readonly FakeBuilder _builder;
        private Func<TestParserConfig, IParserMap> _parserMapFactory;

        public  class TestParserFactory
       {
           public  TestParser CreateNew(IEnumerable<Prefix> prefixes , IEnumerable<Infix> infixes = null)
           {
               var config = TestParserConfig.Factory.CreateNew(prefixes, infixes);
               return new TestParser(config, new FakeBuilder());
           }

            
       }
    }
}