using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class OperatorExpression : ISimpleExpression
    {
        private ISimpleExpression _left;

        private ISimpleExpression _right;

        private readonly object _punctuator;

        public OperatorExpression(
            ISimpleExpression left,
            ISimpleExpression right,
            object punctuator)
        {
            _left = left;
            _right = right;
            _punctuator = punctuator ?? throw new ParseException("Invalid punctuator");
        }
        public void Print(IBuilder builder)
        {
            builder.Append("(");
            _left.Print(builder);
            builder.Append(" ").Append(_punctuator).Append(" ");
            _right.Print(builder);
            builder.Append(")");
        }
    }
}
