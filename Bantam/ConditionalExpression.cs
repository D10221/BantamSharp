
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ISimpleExpression
    {
        public ConditionalExpression(
           ISimpleExpression condition, ISimpleExpression then, ISimpleExpression @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

        private ISimpleExpression Condition { get; }

        private ISimpleExpression Then { get; }

        private ISimpleExpression Else { get; }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            Condition.Print(builder);
            builder.Append("?");
            Then.Print(builder);
            builder.Append(":");
            Else.Print(builder);
            builder.Append(")");
        }
    }
}