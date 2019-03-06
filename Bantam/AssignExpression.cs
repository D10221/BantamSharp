using SimpleParser;
using System;

namespace Bantam
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression<char>
    {
        protected string Name { get; private set; }
        protected ISimpleExpression<char> Right { get; private set; }

        public AssignExpression(String name, ISimpleExpression<char> right)
        {
            Name = name;
            Right = right;
        }      
        public void Print(IBuilder<char> builder)
        {
            builder.Append("(").Append(Name).Append(" = ");
            Right.Print(builder);
            builder.Append(")");
        }
    }
}