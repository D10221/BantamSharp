using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class OperatorExpression : ISimpleExpression
    {
        public OperatorExpression(ISimpleExpression left, TokenType @operator, ISimpleExpression right)
        {
            _left = left;
            _operator = @operator;
            _right = right;
            _punctuator = _operator.Punctuator();
            if (!_punctuator.IsValidPunctuator()) throw new ParseException("Not A valid oprator");
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            _left.Print(builder);
            builder.Append(" ").Append(_punctuator).Append(" ");
            _right.Print(builder);
            builder.Append(")");
        }

        private readonly ISimpleExpression _left;
        private readonly TokenType _operator;
        private readonly ISimpleExpression _right;
        private readonly char _punctuator;
    }
}
