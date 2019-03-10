
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class PostfixOperatorParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;
        public int Precedence { get; }

        public PostfixOperatorParselet(TokenType tokenType, int precedence)
        {
            TokenType = tokenType;
            Precedence = precedence;
        }

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            return new PostfixExpression(token, left);
        }
    }
}
