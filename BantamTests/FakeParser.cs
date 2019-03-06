using Bantam;
using SimpleParser;

namespace BantamTests
{
    public class FakeParser : IParser<Bantam.TokenType, char>
    {
        private readonly NameExpression _expression;

        public FakeParser(NameExpression expression)
        {
            _expression = expression;            
        }


        public ISimpleExpression<char> ParseExpression(int precedence)
        {
            return _expression;
        }

        public bool IsMatch(TokenType expected)
        {
            throw new System.NotImplementedException();
        }

        public IToken<TokenType> Consume(TokenType expected)
        {
            throw new System.NotImplementedException();
        }

        public IToken<TokenType> Consume()
        {
            throw new System.NotImplementedException();
        }
    }
}