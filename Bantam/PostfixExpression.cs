using SimpleParser.Expressions;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace Bantam
{
    /// <summary>
    ///     A postfix unary arithmetic expression like "a!".
    /// </summary>
    public class PostfixExpression : PostfixExpressionBase<TokenType, char>
    {
        public PostfixExpression(ITokenConfig TokenConfig,ISimpleExpression left, TokenType @operator) : base(TokenConfig, left, @operator)
        {
        }

        public override void Print(IBuilder builder)
        {
            builder.Append("(");
            Left.Print(builder);
            builder.Append(Punctuator).Append(")");
        }

    }
}
