using Bantam.Expressions;
using SimpleParser;

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