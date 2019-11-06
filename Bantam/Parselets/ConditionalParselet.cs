
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
            var thenArm = parser.Parse();
            var next  = parser.Consume();
            if(next.TokenType != TokenType.COLON) 
            throw new ParseException("Expected COLON");
            //WHy  precedence -1 
            var elseArm = parser.Parse(Precedence - 1);
            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence { get; }
    }
}
