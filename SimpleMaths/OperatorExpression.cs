using SimpleParser.Expressions;
using ITokenConfig = SimpleParser.ITokenConfig<SimpleMaths.TokenType, string>;
using IBuilder = SimpleParser.IBuilder<string>;
using ISimpleExpression = SimpleParser.ISimpleExpression<string>;

namespace SimpleMaths
{
    /// <summary>
    ///     A binary arithmetic expression like "a + b" or "c ^ d"
    /// </summary>
    public class OperatorExpression : OperatorExpressionBase<TokenType, string>
    {
        public OperatorExpression(ITokenConfig 
            tokenConfig,ISimpleExpression left, TokenType @operator, ISimpleExpression right) 
            : base(tokenConfig,left,@operator,right)
        {
            
        }

        public override void Print(IBuilder builder)
        {
            builder.Append(ToString());
        }

        public override string ToString()
        {
            var func = ((TokenConfig) TokenConfig).GetFunc(Punctuator);
            var l = int.Parse(Left.ToString());
            var r = int.Parse(Right.ToString());
            var result = func(l, r);
            return result.ToString("G");
        }

       
        
    }
}
