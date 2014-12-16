using Bantam;
using Bantam.Expressions;
using SimpleParser;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>;

namespace BantamTests.Support
{
    public class FakeParser : IParser
    {
        private readonly NameExpression _expression;
        private readonly IParserMap _parserMap;

        public FakeParser(NameExpression expression, IParserMap parserMap)
        {
            _expression = expression;
            _parserMap = parserMap;
        }


        public ISimpleExpression ParseExpression(Precedence precedence)
        {

            return _expression;
        }

        public bool IsMatch(TokenType expected)
        {
            throw new System.NotImplementedException();
        }

        public IToken Consume(TokenType expected)
        {
            throw new System.NotImplementedException();
        }

        public IToken Consume()
        {
            throw new System.NotImplementedException();
        }
    }
}