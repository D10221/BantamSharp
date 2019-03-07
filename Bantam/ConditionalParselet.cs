
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : InfixParselet<TokenType>
    {
        public ISimpleExpression Parse(IParser<TokenType> parser, ISimpleExpression left, IToken<TokenType> token)
        {
            var thenArm = parser.ParseExpression(/*Precedence.ZERO*/);
            parser.Consume(TokenType.COLON);
            //WHy  precedence -1 
            var elseArm = parser.ParseExpression(Precedence - 1);

            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence { get; } = (int)Bantam.Precedence.CONDITIONAL;
    }
}
