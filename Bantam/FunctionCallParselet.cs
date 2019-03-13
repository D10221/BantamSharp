
using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class FunctionCallParselet : IParselet<TokenType>
    {
        public FunctionCallParselet(TokenType tokenType, int precedence)
        {
            TokenType = tokenType;
            Precedence = precedence;
        }

        public TokenType TokenType { get; }
        public int Precedence { get; }
        public ParseletType ParseletType { get; } = ParseletType.Infix;

        public ISimpleExpression<TokenType> Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression<TokenType> left)
        {
            // Parse the comma-separated arguments until we hit, ")".
            var args = new List<ISimpleExpression<TokenType>>();

            // There may be no arguments at all.
            if (parser.IsMatch(TokenType.PARENT_RIGHT))
                return new FunctionCallExpression(left, args);
            do
            {
                args.Add(parser.ParseExpression((int) Bantam.Precedence.ZERO, this));
            } while (parser.IsMatch(TokenType.COMMA));

            parser.Consume(TokenType.PARENT_RIGHT);

            return new FunctionCallExpression(left, args);
        }
    }
}