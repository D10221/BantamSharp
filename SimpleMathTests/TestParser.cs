using System.Collections.Generic;
using SimpleMaths;
using SimpleParser;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.IParselet<SimpleMaths.TokenType, string>>;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.InfixParselet<SimpleMaths.TokenType, string>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleMaths.TokenType, string>;
using ParserMap = SimpleParser.ParserMap<SimpleMaths.TokenType, string>;
using Parser = SimpleParser.Parser<SimpleMaths.TokenType, string>;
using IBuilder = SimpleParser.IBuilder<string>;


namespace SimpleMathTests
{
    public class TestParser
    {
        private readonly IParserConfig<TokenType, string> _config;

        public TestParser(IParserConfig<TokenType, string> config, IBuilder builder)
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
        private readonly IBuilder _builder;

        public  class TestParserFactory
       {
           public  TestParser CreateNew(IEnumerable<Prefix> prefixes , IEnumerable<Infix> infixes = null)
           {
               var config = ParserConfig.Factory.CreateNew(prefixes, infixes);
               return new TestParser(config, new Builder());
           }

            
       }
    }
}