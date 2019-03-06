using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class CallExpression: ISimpleExpression<char>
    {
        public CallExpression(ISimpleExpression<char> function, List<ISimpleExpression<char>> args)
        {
            Function = function;

            Args = args ?? new List<ISimpleExpression<char>>();
        }

        public void Print(IBuilder<char> builder)
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

        protected ISimpleExpression<char> Function { get; private set; }

        protected List<ISimpleExpression<char>> Args { get; private set; }

    }
}