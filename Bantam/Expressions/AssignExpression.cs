using System;
using SimpleParser;

namespace Bantam.Expressions
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression {
        public AssignExpression(String name, ISimpleExpression right) {
            _name = name;
            _right = right;
        }
  
        public void Print(IBuilder builder) {
            builder.Append("(").Append(_name).Append(" = ");
            _right.Print(builder);
            builder.Append(")");
        }

        private readonly String _name;
        private readonly ISimpleExpression _right;
    }
}