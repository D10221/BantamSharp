using System;
using SimpleParser.Expressions;
using IBuilder = SimpleParser.IBuilder<char>;

namespace Bantam.Expressions
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : NameExpressionBase<char> {
        public NameExpression(string name) : base(name)
        {
        }
        public override void Print(IBuilder builder)
        {
            builder.Append(Name);
        }
    }
}