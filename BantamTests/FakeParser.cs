using Bantam;

using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;

namespace BantamTests
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


        public ISimpleExpression ParseExpression(int precedence)
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