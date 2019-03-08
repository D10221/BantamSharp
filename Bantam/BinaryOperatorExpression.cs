using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class BinaryOperatorExpression : ISimpleExpression
    {
        public object Token {get;}
        public ISimpleExpression Left { get; }
        public ISimpleExpression Right { get; }

        public BinaryOperatorExpression(
            IToken<TokenType> token,
            ISimpleExpression left,
            ISimpleExpression right
            )
        {
            Left = left;
            Right = right;
            Token = token ?? throw new ParseException("Invalid punctuator");
        }
        public void Print(IBuilder builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(Token);
            Right.Print(builder);
            builder.Append(")");
        }
    }
}
