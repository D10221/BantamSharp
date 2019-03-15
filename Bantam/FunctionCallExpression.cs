using SimpleParser;
using System.Collections.Generic;
using System.Linq;

namespace Bantam
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class FunctionCallExpression : ISimpleExpression<TokenType> 
    {
        public IToken<TokenType> Token { get; }
        /// <summary>
        /// Function 
        /// </summary>
        public ISimpleExpression<TokenType> Left { get; }
        public IEnumerable<ISimpleExpression<TokenType>> Right { get; }

        public FunctionCallExpression(ISimpleExpression<TokenType> left, List<ISimpleExpression<TokenType>> right)
        {
            Left = left;
            Right = right ?? new List<ISimpleExpression<TokenType>>();
        }

        public void Visit(IExpressionVisitor<TokenType> visitor)
        {
            visitor.Visit(this);
        }
    }
}