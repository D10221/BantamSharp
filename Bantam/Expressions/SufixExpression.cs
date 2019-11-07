using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class SufixExpression : ISimpleExpression<TokenType> 
    {
        public IToken<TokenType> Token { get; }

        public ISimpleExpression<TokenType> Left { get; }

        public SufixExpression(IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            Token = token;
            Left = left;
        }
        public override string ToString() {
            return $"{Left}{Token}";
        } 
    }
}
