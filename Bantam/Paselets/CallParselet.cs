using System.Collections.Generic;
using Bantam.Expressions;
using SimpleParser;
using SimpleParser.Parselets;
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

namespace Bantam.Paselets
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class CallParselet : InfixParselet<TokenType, char>
    {
        public ISimpleExpression Parse(IParser parser, ISimpleExpression left, IToken token)
        {
            // Parse the comma-separated arguments until we hit, ")".
            var args = new List<ISimpleExpression>();

            // There may be no arguments at all.
            if (parser.IsMatch(TokenType.RIGHT_PAREN))
                return new CallExpression(left, args);
            do
            {
                args.Add(parser.ParseExpression(/*Precedence.ZERO*/));
            } while (parser.IsMatch(TokenType.COMMA));

            parser.Consume(TokenType.RIGHT_PAREN);

            return new CallExpression(left, args);
        }

        public Precedence Precedence
        {
            get { return Precedence.CALL; }
        }
    }
}
