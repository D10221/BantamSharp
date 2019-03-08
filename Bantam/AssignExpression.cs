using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression
    {
        public object Token { get; }
        public ISimpleExpression Left { get; }
        public ISimpleExpression Right { get; }

        public AssignExpression(ISimpleExpression left, ISimpleExpression right)
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