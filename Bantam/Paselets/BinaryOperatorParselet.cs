using Bantam.Expressions;
using SimpleParser;

namespace Bantam.Paselets
{
  
    /// <summary>
    /// Generic infix parselet for a binary arithmetic operator. The only
    /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// </summary>
    public class BinaryOperatorParselet : InfixParselet
    {
        public BinaryOperatorParselet(Precedence precedence, bool isRight)
        {
            _precedence = precedence;
            _right = isRight;
        }

        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.ParseExpression();

            return new OperatorExpression(left, token.TokenType, right);
        }

        public int Precedence
        {
            get { return (int) _precedence; }
        }

        private readonly Precedence _precedence;
        private readonly bool _right;
    }
}
