using System.Collections.Generic;
using Bantam.Expressions;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleParser.TokenType>;
using IPrefixParselet = SimpleParser.IPrefixParselet<SimpleParser.TokenType>;
using IToken = SimpleParser.IToken<SimpleParser.TokenType>;
using InfixParselet = SimpleParser.InfixParselet<SimpleParser.TokenType>;

namespace Bantam.Paselets
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class CallParselet : InfixParselet
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
