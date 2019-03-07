using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class OperatorExpression : ISimpleExpression
    {
        private ISimpleExpression _left ;

        private ISimpleExpression _right ;

        private char Punctuator { get; set; }

        public OperatorExpression(
            IDictionary<TokenType, char> tokenTypes,
            TokenType tokenType,
            ISimpleExpression left,
            ISimpleExpression right)
        {
            _left = left;
            _right = right;
            if (!tokenTypes.TryGetValue(tokenType, out var x))
            {
                throw new ParseException($"Invalid tokenType: '{tokenType.ToString()}'");
            }
            Punctuator = x;
        }
        public void Print(IBuilder builder)
        {
            builder.Append("(");
            _left.Print(builder);
            builder.Append(" ").Append(Punctuator).Append(" ");
            _right.Print(builder);
            builder.Append(")");
        }
    }
}
