


using System.Collections.Generic;

namespace uParserTests
{
    /// <summary>
    /// Generic infix parselet for a binary arithmetic operator. The only
    /// difference when parsing, "+", "-", "*", "/", and "^" is precedence and
    /// associativity, so we can use a single parselet class for all of those.
    /// </summary>
    public class BinaryOperatorParselet : InfixParselet
    {
        public static BinaryOperatorParselet Right(int precedence)
        {
            return new BinaryOperatorParselet(precedence, true);
        }
        public static BinaryOperatorParselet Left(int precedence)
        {
            return new BinaryOperatorParselet(precedence, false);
        }
        public int Precedence { get; }

        public bool IsRight { get; }

        public BinaryOperatorParselet(int precedence, bool isRight)
        {
            Precedence = precedence;
            IsRight = isRight;
        }

        public ISimpleExpression Parse(
                Parser parser,
                IList<Token> lexer,
                Token token, ISimpleExpression left)
        {
            // To handle right-associative operators like "^", we allow a slightly
            // lower precedence when parsing the right-hand side. This will let a
            // parselet with the same precedence appear on the right, which will then
            // take *this* parselet's result as its left-hand argument.
            var right = parser.Parse(Precedence - (IsRight ? 1 : 0));
            return new BinaryOperatorExpression(token, left, right);
        }
    }
}
