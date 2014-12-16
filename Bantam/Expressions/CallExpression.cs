using System.Collections.Generic;
using SimpleParser;
using SimpleParser.Expressions;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
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
using CallExpressionBase = SimpleParser.Expressions.CallExpressionBase<char>;

namespace Bantam.Expressions
{
    /// <summary>
    /// A function call like "a(b, c, d) OR a(x)(z)".
    /// </summary>
    public class CallExpression : CallExpressionBase<char>
    {
        public CallExpression(ISimpleExpression function, List<ISimpleExpression> args) : base(function, args)
        {
        }

        public override void Print(IBuilder builder)
        {
            Function.Print(builder);
            builder.Append("(");
            for (var i = 0; i < Args.Count; i++)
            {
                Args[i].Print(builder);
                if (i < Args.Count - 1) builder.Append(", ");
            }
            builder.Append(")");
        }
    }
}