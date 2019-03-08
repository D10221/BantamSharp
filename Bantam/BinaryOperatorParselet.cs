
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// Generic infix parselet for a binary arithmetic operator. The only
    /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// </summary>
    public class BinaryOperatorParselet : InfixParselet<TokenType>
    {
        public int Precedence { get; }

        private readonly bool _isRight;

        public BinaryOperatorParselet(int precedence, InfixType infixType)
        {
            Precedence = precedence;
            _isRight = infixType == InfixType.Right;
        }

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression(Precedence - (_isRight ? 1 : 0));
            return new BinaryOperatorExpression(left, right, token);
        }
    }
}
