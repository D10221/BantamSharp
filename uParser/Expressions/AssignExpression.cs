

namespace uParser
{
    /// <summary>
    /// An assignment expression like "a = b"
    /// </summary>
    public class AssignExpression : ISimpleExpression
    {
        public Token Token { get; }
        public ISimpleExpression Left { get; }
        public ISimpleExpression Right { get; }

        public AssignExpression(ISimpleExpression left, ISimpleExpression right)
        {
            Left = left;
            Token = ((NameExpression)left).Token;
            Right = right ?? throw new ParseException($"Missing right side expression from {Left}");
        }
        public override string ToString()
        {
            return $"{Token}";
        }
    }
}