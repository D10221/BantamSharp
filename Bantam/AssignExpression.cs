using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression<TokenType>
    {
        public IToken<TokenType> Token { get; }
        public ISimpleExpression<TokenType> Left { get; }
        public ISimpleExpression<TokenType> Right { get; }

        public AssignExpression(ISimpleExpression<TokenType> left, ISimpleExpression<TokenType> right)
        {
            Left = left;
            Token = ((NameExpression)left).Token;
            Right = right ?? throw new ParseException($"Missing right side expression from {Left}");
        }

        public void Print(IBuilder builder)
        {
            builder
                .Append("(");
            builder.Append(Token);
            builder.Append("=");
            Right.Print(builder);
            builder.Append(")");
        }
    }
}