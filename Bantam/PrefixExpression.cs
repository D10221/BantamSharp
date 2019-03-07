using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : ISimpleExpression
    {
        readonly object _punctuator;
        ISimpleExpression _right;

        public PrefixExpression(
            object punctuator,
            ISimpleExpression right)
        {
            _punctuator = punctuator;
            _right = right;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(").Append(_punctuator);
            _right.Print(builder);
            builder.Append(")");
        }
    }
}
