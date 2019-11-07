

namespace uParserTests
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : ISimpleExpression 
    {
        public Token Token { get; }
        public ISimpleExpression Right { get; }

        public PrefixExpression(
            Token token,
            ISimpleExpression right)
        {
            Token = token;
            Right = right;
        }     
        public override string ToString() {
            return $"{Token}{Right}";
        }   
    }
}
