using System;
using System.Text;

namespace Bantam.Expressions
{
    /**
 * An assignment expression like "a = b".
 */
    public class AssignExpression : Expression {
        public AssignExpression(String name, Expression right) {
            _name = name;
            mRight = right;
        }
  
        public void Print(IBuilder builder) {
            builder.Append("(").Append(_name).Append(" = ");
            mRight.Print(builder);
            builder.Append(")");
        }

        private readonly String _name;
        private readonly Expression mRight;
    }
}