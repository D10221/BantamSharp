using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
using IToken = SimpleParser.IToken<SimpleParser.TokenType>;
using InfixParselet = SimpleParser.InfixParselet<SimpleParser.TokenType>;
using IParserMap= SimpleParser.IParserMap<SimpleParser.TokenType>;

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