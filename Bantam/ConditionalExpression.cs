
using SimpleParser;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace Bantam
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ConditionalExpressionBase<char>
    {
        public ConditionalExpression(
            ISimpleExpression condition, ISimpleExpression then, ISimpleExpression @else) : base(condition, then, @else)
        {
        }
        public override void Print(IBuilder builder)
        {
            builder.Append("(");
            Condition.Print(builder);
            builder.Append(" ? ");
            Then.Print(builder);
            builder.Append(" : ");
            Else.Print(builder);
            builder.Append(")");
        }
    }
}