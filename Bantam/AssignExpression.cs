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
            this.Left = left;
            Token = ((NameExpression)left).Token;
            Right = right;
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