using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixSimpleExpression : ISimpleExpression
    {
        public PostfixSimpleExpression(ISimpleExpression left, TokenType @operator)
        {
            mLeft = left;
            mOperator = @operator;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            mLeft.Print(builder);
            builder.Append(mOperator.Punctuator()).Append(")");
        }

        private readonly ISimpleExpression mLeft;
        private readonly TokenType mOperator;
    }
}
