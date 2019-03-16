using SimpleParser;

namespace Bantam
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class BinaryOperatorExpression : ISimpleExpression<TokenType>
    {
        public IToken<TokenType> Token { get; }
        public ISimpleExpression<TokenType> Left { get; }
        public ISimpleExpression<TokenType> Right { get; }

        public BinaryOperatorExpression(
            IToken<TokenType> token,
            ISimpleExpression<TokenType> left,
            ISimpleExpression<TokenType> right
            )
        {
            Left = left;
            Right = right;
            Token = token ?? throw new ParseException("Invalid punctuator");
        }       
    }
}
