using System.Collections.Generic;
using Bantam;
using SimpleParser;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;


namespace BantamTests
{
    public class TestParser
    {
        private readonly IParserConfig<TokenType, char> _config;

        public TestParser(IParserConfig<TokenType, char> config, IBuilder builder, TokenConfig tokenConfig)
        {
            _config = config;
            _builder = builder;
            _tokenConfig = tokenConfig;
        }

        public  string Parse(string source)
        {            
            var lexer = new Lexer(source, _tokenConfig);
            var parserMap = new ParserMap(_config.PrefixParselets,_config.InfixParselets);
            var parser = new Parser(lexer,parserMap);
            var result = parser.ParseExpression();
           
            result.Print(_builder);
            var actual = _builder.Build();
            return actual;
        }

        private readonly IBuilder _builder;
        private readonly TokenConfig _tokenConfig;
        
        public static readonly TestParserFactory Factory =  new TestParserFactory();

        public  class TestParserFactory
       {
           public  TestParser CreateNew(IEnumerable<Prefix> prefixes, IEnumerable<Infix> infixes = null)
           {
               var config = ParserConfig.Factory.CreateNew(prefixes, infixes);
               return new TestParser(config, new FakeBuilder(), new TokenConfig());
           }

            
       }
    }
}