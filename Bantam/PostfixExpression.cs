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

        private char Punctuator { get; }

        public PostfixExpression(IDictionary<TokenType, char> tokenTypes, ISimpleExpression left, TokenType tokenType)
        {
            Left = left;
            tokenTypes.TryGetValue(tokenType, out var type);
            Punctuator = type;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(Punctuator.ToString()).Append(")");
        }
    }
}
