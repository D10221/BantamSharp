
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : IParselet<TokenType>
    {
        public ConditionalParselet(TokenType tokenType, int precedence)
        {
            TokenType = tokenType;
            Precedence = precedence;
        }

        public TokenType TokenType { get; set; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;
        public ISimpleExpression<TokenType> Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            var thenArm = parser.ParseExpression((int)Bantam.Precedence.ZERO, this);
            parser.Consume(TokenType.COLON);
            //WHy  precedence -1 
            var elseArm = parser.ParseExpression(Precedence - 1, this);
            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence { get; }
    }
}
