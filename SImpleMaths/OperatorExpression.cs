using SimpleParser;
using SimpleParser.Expressions;
using ParseException = SimpleParser.ParseException<SimpleMaths.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<SimpleMaths.TokenType, string>;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>>;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType, string>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleMaths.TokenType, string>;
using ParserMap = SimpleParser.ParserMap<SimpleMaths.TokenType, string>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType, string>;
using Parser = SimpleParser.Parser<SimpleMaths.TokenType, string>;
using IBuilder = SimpleParser.IBuilder<string>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<string>;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType, string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;

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
