
using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class PostfixOperatorParselet : InfixParselet<TokenType>
    {
        private readonly IDictionary<TokenType, char> _tokenTypes;

        public int Precedence { get; }

        public PostfixOperatorParselet(int precedence, IDictionary<TokenType, char> tokenTypes)
        {
            _tokenTypes = tokenTypes;
            Precedence = precedence;
        }

        public ISimpleExpression Parse(IParser<TokenType> parser, ISimpleExpression left, IToken<TokenType> token)
        {
            return new PostfixExpression(_tokenTypes, left, token.TokenType);
        }
    }
}
