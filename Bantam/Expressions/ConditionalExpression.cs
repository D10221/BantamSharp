using System.Text;

namespace Bantam.Expressions
{
    /**
* A ternary conditional expression like "a ? b : c".
*/
    public class ConditionalExpression : Expression {
        public ConditionalExpression(
            Expression condition, Expression thenArm, Expression elseArm) {
            mCondition = condition;
            mThenArm   = thenArm;
            mElseArm   = elseArm;
            }
  
        public void Print(IBuilder builder) {
            builder.Append("(");
            mCondition.Print(builder);
            builder.Append(" ? ");
            mThenArm.Print(builder);
            builder.Append(" : ");
            mElseArm.Print(builder);
            builder.Append(")");
        }

        private readonly Expression mCondition;
        private readonly Expression mThenArm;
        private readonly Expression mElseArm;
    }
}