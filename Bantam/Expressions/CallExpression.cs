using System.Collections.Generic;
using System.Text;

namespace Bantam.Expressions
{
    /**
 * A function call like "a(b, c, d)".
 */

    public class CallExpression : Expression
    {
        public CallExpression(Expression function, List<Expression> args)
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

        private readonly Expression mFunction;
        private readonly List<Expression> mArgs;
    }
}