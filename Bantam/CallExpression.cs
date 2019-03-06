using System.Collections.Generic;
using SimpleParser;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace Bantam
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class CallExpression : CallExpressionBase<char>
    {
        public CallExpression(ISimpleExpression function, List<ISimpleExpression> args) : base(function, args)
        {
        }

        public override void Print(IBuilder builder)
        {
            Function.Print(builder);
            builder.Append("(");
            for (var i = 0; i < Args.Count; i++)
            {
                Args[i].Print(builder);
                if (i < Args.Count - 1) builder.Append(", ");
            }
            builder.Append(")");
        }
    }
}