using SimpleParser;
using SimpleParser.Expressions;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<char>;
using IParser= SimpleParser.IParser<Bantam.TokenType,char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using ConditionalExpressionBase = SimpleParser.Expressions.ConditionalExpressionBase<char>;

namespace Bantam.Expressions
{
    /// <summary>
    ///     A ternary conditional expression like "a ? b : c".
    /// </summary>
    public class ConditionalExpression : ConditionalExpressionBase<char>
    {
        public ConditionalExpression(
            ISimpleExpression condition, ISimpleExpression then, ISimpleExpression @else) : base(condition, then, @else)
        {
        }
        public override void Print(IBuilder builder)
        {
            builder.Append("(");
            Condition.Print(builder);
            builder.Append(" ? ");
            Then.Print(builder);
            builder.Append(" : ");
            Else.Print(builder);
            builder.Append(")");
        }
    }
}