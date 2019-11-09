
using System.Collections.Generic;
using System.Linq;

namespace uParser
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class FunctionCallExpression : ISimpleExpression 
    {
        public Token Token { get; }
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
        public override string ToString()
        {
            return $"{Token}";
        }
    }
}