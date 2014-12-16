using System.Collections.Generic;
using Bantam;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType,char>> ;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType,char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType,char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType,char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType,char>;
using Parser = SimpleParser.Parser<Bantam.TokenType,char>;
using IBuilder = SimpleParser.IBuilder<char>;


namespace BantamTests.Support
{
    public class TestParser
    {
        private readonly ParserConfig _config;

        public TestParser(ParserConfig config, IBuilder builder)
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
               return new TestParser(config, new FakeBuilder());
           }

            
       }
    }
}