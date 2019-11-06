
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for an unary arithmetic operator. Parses postfix
    /// unary "?" expressions.
    /// </summary>
    public class SufixOperatorParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;
        public int Precedence { get; }

        public SufixOperatorParselet(TokenType tokenType, int precedence)
        {
            TokenType = tokenType;
            Precedence = precedence;
        }

        public ISimpleExpression<TokenType> Parse(
            IParser<TokenType> parser, 
            ILexer<IToken<TokenType>> lexer,
            IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            return new SufixExpression(token, left);
        }
    }
}
