

namespace uParserTests
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class BinaryOperatorExpression : ISimpleExpression
    {
        public Token Token { get; }
        public ISimpleExpression Left { get; }
        public ISimpleExpression Right { get; }

        public BinaryOperatorExpression(
            Token token,
            ISimpleExpression left,
            ISimpleExpression right
            )
        {
            Left = left;
            Right = right;
            Token = token ?? throw new ParseException("Invalid punctuator");
        }       
        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
