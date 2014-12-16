using SimpleParser;
using SimpleParser.Expressions;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>;
using OperatorExpressionBase = SimpleParser.Expressions.OperatorExpressionBase<Bantam.TokenType,char>;

namespace Bantam.Expressions
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
