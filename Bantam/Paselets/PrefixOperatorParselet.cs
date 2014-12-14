using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    /// <summary>
    ///     Generic prefix parselet for an unary arithmetic operator. Parses prefix
    ///     unary "-", "+", "~", and "!" expressions.
    /// </summary>
    public class PrefixOperatorParselet : IPrefixParselet, IParselet
    {
        public PrefixOperatorParselet(Precedence precedence)
        {
            mPrecedence = precedence;
        }

        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression();

            return new PrefixExpression(token.GetTokenType(), right);
        }

        public int getPrecedence()
        {
            return (int) mPrecedence;
        }

        private readonly Precedence mPrecedence;
    }
}
