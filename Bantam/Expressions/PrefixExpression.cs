using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixSimpleExpression : ISimpleExpression
    {
        public PrefixSimpleExpression(TokenType @operator, ISimpleExpression right)
        {
            mOperator = @operator;
            mRight = right;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(").Append(mOperator.Punctuator());
            mRight.Print(builder);
            builder.Append(")");
        }

        private readonly TokenType mOperator;
        private readonly ISimpleExpression mRight;
    }
}
