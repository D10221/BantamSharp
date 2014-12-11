using System.Collections.Generic;
using System.Text;

namespace Bantam.Expressions
{
    /**
 * A function call like "a(b, c, d)".
 */

    public class CallSimpleExpression : ISimpleExpression
    {
        public CallSimpleExpression(ISimpleExpression function, List<ISimpleExpression> args)
        {
            mFunction = function;
            mArgs = args;
        }

        public void Print(IBuilder builder)
        {
            mFunction.Print(builder);
            builder.Append("(");
            for (var i = 0; i < mArgs.Count; i++)
            {
                mArgs[i].Print(builder);
                if (i < mArgs.Count - 1) builder.Append(", ");
            }
            builder.Append(")");
        }

        private readonly ISimpleExpression mFunction;
        private readonly List<ISimpleExpression> mArgs;
    }
}