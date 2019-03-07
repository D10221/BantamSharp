using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class CallExpression : ISimpleExpression
    {
        public CallExpression(ISimpleExpression function, List<ISimpleExpression> args)
        {
            Function = function;

            Args = args ?? new List<ISimpleExpression>();
        }

        public void Print(IBuilder builder)
        {
            Function?.Print(builder);
            builder.Append("(");
            for (var i = 0; i < Args.Count; i++)
            {
                Args[i].Print(builder);
                if (i < Args.Count - 1) builder.Append(", ");
            }
            builder.Append(")");
        }

        protected ISimpleExpression Function { get; private set; }

        protected List<ISimpleExpression> Args { get; private set; }
    }
}