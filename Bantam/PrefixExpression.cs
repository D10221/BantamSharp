using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : ISimpleExpression
    {
        public object Token {get;}
        public ISimpleExpression Right { get; }

        public PrefixExpression(
            IToken<TokenType> token,
            ISimpleExpression right)
        {
            Token = token;
            Right = right;
        }

        public void Print(IBuilder builder)
        {
            builder
                .Append("(");
            builder.Append(Token);
            Right.Print(builder);
            builder.Append(")");
        }
    }
}
