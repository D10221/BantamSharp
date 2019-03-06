using SimpleParser.Expressions;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace Bantam
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class OperatorExpression : OperatorExpressionBase<TokenType, char>
    {
        public OperatorExpression(ITokenConfig 
            tokenConfig,ISimpleExpression left, TokenType @operator, ISimpleExpression right) : base(tokenConfig, left, @operator, right)
        {
        }
        public override void Print(IBuilder builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(" ").Append(Punctuator).Append(" ");
            Right.Print(builder);
            builder.Append(")");

        }
    }
}
