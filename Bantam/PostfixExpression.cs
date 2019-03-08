using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixExpression : ISimpleExpression
    {
        public object Token { get; }

        public ISimpleExpression Left { get; }

        public PostfixExpression(IToken<TokenType> token, ISimpleExpression left)
        {
            Token = token;
            Left = left;
        }

        public void Print(IBuilder builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(Token);
            builder.Append(")");
        }
    }
}
