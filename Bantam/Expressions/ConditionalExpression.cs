using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ISimpleExpression
    {
        public ConditionalExpression(
            ISimpleExpression condition, ISimpleExpression then, ISimpleExpression @else)
        {
            _condition = condition;
            _then = then;
            _else = @else;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            _condition.Print(builder);
            builder.Append(" ? ");
            _then.Print(builder);
            builder.Append(" : ");
            _else.Print(builder);
            builder.Append(")");
        }

        private readonly ISimpleExpression _condition;
        private readonly ISimpleExpression _then;
        private readonly ISimpleExpression _else;
    }
}