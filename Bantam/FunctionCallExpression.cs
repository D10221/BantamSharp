using SimpleParser;
using System.Collections.Generic;
using System.Linq;

namespace Bantam
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class FunctionCallExpression : ISimpleExpression
    {
        public object Token {get;}
        /// <summary>
        /// Function 
        /// </summary>
        public ISimpleExpression Left { get; }
        public IEnumerable<ISimpleExpression> Right { get; }

        public FunctionCallExpression(ISimpleExpression left, List<ISimpleExpression> right)
        {
            Left = left;
            Right = right ?? new List<ISimpleExpression>();
        }

        public void Print(IBuilder builder)
        {
            Left?.Print(builder);
            builder.Append("(");
            int count = Right.Count();
            // var i = 0; i < count; i++
            var i = 0;
            foreach (var arg in Right)
            {
                arg.Print(builder);
                if (++i < count) builder.Append(",");
            }
            builder.Append(")");
        }

    }
}