using SimpleParser;
using System.Collections.Generic;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace Bantam
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class OperatorExpression : ISimpleExpression<char>
    {
        ISimpleExpression<char> Left { get; }

        ISimpleExpression<char> Right { get; }

        char Punctuator { get; set; }

        public OperatorExpression(
            IDictionary<TokenType, char> tokenTypes,
            TokenType tokenType,
            ISimpleExpression left,
            ISimpleExpression right)
        {
            Left = left;
            Right = right;
            if (!tokenTypes.TryGetValue(tokenType, out var x))
            {
                throw new ParseException<TokenType>($"Invalid tokenType: '{tokenType.ToString()}'");
            }
            Punctuator = x;
        }
        public void Print(IBuilder<char> builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(" ").Append(Punctuator).Append(" ");
            Right.Print(builder);
            builder.Append(")");
        }
    }
}
