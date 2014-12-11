using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalSimpleExpression : ISimpleExpression
    {
        public ConditionalSimpleExpression(
            ISimpleExpression condition, ISimpleExpression thenArm, ISimpleExpression elseArm)
        {
            mCondition = condition;
            mThenArm = thenArm;
            mElseArm = elseArm;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            mCondition.Print(builder);
            builder.Append(" ? ");
            mThenArm.Print(builder);
            builder.Append(" : ");
            mElseArm.Print(builder);
            builder.Append(")");
        }

        private readonly ISimpleExpression mCondition;
        private readonly ISimpleExpression mThenArm;
        private readonly ISimpleExpression mElseArm;
    }
}