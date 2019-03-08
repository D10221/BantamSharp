
using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ISimpleExpression
    {
        public object Token { get; }
        public ISimpleExpression Condition { get; }
        public ISimpleExpression Then { get; }
        public ISimpleExpression Else { get; }

        public ConditionalExpression(
           ISimpleExpression condition, ISimpleExpression then, ISimpleExpression @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }

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