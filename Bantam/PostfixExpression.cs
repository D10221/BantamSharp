using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixExpression : ISimpleExpression
    {
        private ISimpleExpression Left { get; }

        private object _token;

        public PostfixExpression(object token, ISimpleExpression left)
        {
            Left = left;            
            _token = token;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(_token).Append("(");
        }
    }
}
