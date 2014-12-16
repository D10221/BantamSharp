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
    /// A prefix unary arithmetic expression like "!a" or "-b".
    /// </summary>
    public class PrefixExpression : PrefixExpressionBase<TokenType,string>
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