using System;
using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignSimpleExpression : ISimpleExpression {
        public AssignSimpleExpression(String name, ISimpleExpression right) {
            _name = name;
            mRight = right;
        }
  
        public void Print(IBuilder builder) {
            builder.Append("(").Append(_name).Append(" = ");
            mRight.Print(builder);
            builder.Append(")");
        }

        private readonly String _name;
        private readonly ISimpleExpression mRight;
    }
}