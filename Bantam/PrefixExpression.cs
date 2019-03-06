
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using SimpleParser;

namespace Bantam
{
    /// <summary>
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : PrefixExpressionBase<TokenType, char>
    {
        public PrefixExpression(ITokenConfig tokenConfig,TokenType @operator, ISimpleExpression right) 
            : base(tokenConfig,@operator,right)
        {
           
        }

        public override void Print(IBuilder builder)
        {
            builder.Append("(").Append(Punctuator);
            Right.Print(builder);
            builder.Append(")");
        }

        
    }
}
