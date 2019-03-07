
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class PostfixOperatorParselet : InfixParselet<TokenType>
    {
        public int Precedence { get; }

        public PostfixOperatorParselet(int precedence)
        {
            Precedence = precedence;
        }

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            return new PostfixExpression(left, token);
        }
    }
}
