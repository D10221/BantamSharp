using System;
using SimpleParser;
using IBuilder = SimpleParser.IBuilder<char>;

namespace Bantam
{
    /// <summary>
    /// A simple variable name expression like "abc".
    /// </summary>
    public class NameExpression : NameExpressionBase<char>
    {
        public NameExpression(string name) : base(name)
        {
        }
        public override void Print(IBuilder builder)
        {
            builder.Append(Name);
        }
    }
}