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
        
        bool UseEnclosure
        {
            get
            {
                return !(this.Token as IToken<TokenType>)?.TokenType.Equals(TokenType.AT) ?? false;
            }
        }

        public void Print(IBuilder builder)
        {
            if (UseEnclosure) builder.Append("(");
            builder.Append(Token);
            Right.Print(builder);
            if (UseEnclosure) builder.Append(")");
        }
    }
}
