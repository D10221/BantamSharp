using SimpleParser;
using System;

namespace Bantam
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression
    {
        public object Name { get;  }

        public ISimpleExpression Right { get; }

        public AssignExpression(object name, ISimpleExpression right)
        {
            Name = name;
            Right = right;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(").Append(Name).Append(" = ");
            Right.Print(builder);
            builder.Append(")");
        }
    }
}