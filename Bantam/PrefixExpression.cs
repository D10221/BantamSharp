using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : ISimpleExpression<TokenType> 
    {
        public IToken<TokenType> Token { get; }
        public ISimpleExpression<TokenType> Right { get; }

        public PrefixExpression(
            IToken<TokenType> token,
            ISimpleExpression<TokenType> right)
        {
            Token = token;
            Right = right;
        }        
    }
}
