using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixExpression : ISimpleExpression<char>
    {
        ISimpleExpression<char> Left { get; }

        char Punctuator { get; }

        public PostfixExpression(IDictionary<TokenType, char> tokenTypes, ISimpleExpression<char> left, TokenType tokenType)
        {
            Left = left;
            tokenTypes.TryGetValue(tokenType, out var type);
            Punctuator = type;
        }

        public void Print(IBuilder<char> builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(Punctuator).Append(")");
        }
    }
}
