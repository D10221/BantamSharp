

namespace uParserTests
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixExpression : ISimpleExpression 
    {
        public Token Token { get; }

        public ISimpleExpression Left { get; }

        public PostfixExpression(Token token, ISimpleExpression left)
        {
            Token = token;
            Left = left;
        }
        public override string ToString() {
            return $"{Left}{Token}";
        } 
    }
}
