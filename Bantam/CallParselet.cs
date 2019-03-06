
using SimpleParser;
using System.Collections.Generic;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;

namespace Bantam
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class CallParselet : InfixParselet<TokenType, char>
    {
        public int Precedence { get; } = (int)Bantam.Precedence.CALL;

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
    }
}