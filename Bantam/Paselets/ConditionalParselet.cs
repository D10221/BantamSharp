using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;

namespace Bantam.Paselets
{
    /// <summary>
    ///     Parselet for the condition or "ternary" operator, like "a ? b : c".
    /// </summary>
    public class ConditionalParselet : InfixParselet<TokenType, char>
    {
        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            var thenArm = parser.ParseExpression(/*Precedence.ZERO*/);
            parser.Consume(TokenType.COLON);
            //WHy  precedence -1 
            var elseArm = parser.ParseExpression(Precedence - 1);
            return new ConditionalExpression(left, thenArm, elseArm);
        }

        public int Precedence { get; } = (int) Bantam.Precedence.CONDITIONAL;
    }
}
