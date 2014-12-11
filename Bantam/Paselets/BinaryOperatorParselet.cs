using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
    // difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// <summary>
    ///     Generic infix parselet for a binary arithmetic operator. The only
    /// </summary>
    public class BinaryOperatorParselet : InfixParselet
    {
        public BinaryOperatorParselet(Precedence precedence, bool isRight)
        {
            mPrecedence = precedence;
            mIsRight = isRight;
        }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            ISimpleExpression right = parser.ParseExpression();

            return new OperatorSimpleExpression(left, token.GetTokenType(), right);
        }

        public int GetPrecedence()
        {
            return (int) mPrecedence;
        }

        private readonly Precedence mPrecedence;
        private readonly bool mIsRight;
    }
}
