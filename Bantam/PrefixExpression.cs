
using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : PrefixExpressionBase<TokenType, char>
    {
        public PrefixExpression(IDictionary<TokenType, char> tokenTypes, TokenType @operator, ISimpleExpression<char> right)
            : base(tokenTypes, @operator, right)
        {
        }

        public override void Print(IBuilder<char> builder)
        {
            builder.Append("(").Append(Punctuator);
            Right.Print(builder);
            builder.Append(")");
        }
    }
}
