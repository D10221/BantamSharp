using SimpleParser;
using System;

namespace Bantam
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression
    {
        protected string Name { get; private set; }
        protected ISimpleExpression Right { get; private set; }

        public AssignExpression(String name, ISimpleExpression right)
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