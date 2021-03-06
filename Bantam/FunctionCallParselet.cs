﻿
using SimpleParser;
using System.Collections.Generic;

namespace Bantam
{
    /// <summary>
    ///     Parselet to parse a function call like "a(b, c, d)".
    /// </summary>
    public class FunctionCallParselet : IParselet<TokenType>
    {
        public TokenType TokenType { get; set; }
        public int Precedence { get; } = (int)Bantam.Precedence.CALL;
        public ParseletType ParseletType { get; } = ParseletType.Infix;

        public ISimpleExpression Parse(IParser<TokenType> parser, IToken<TokenType> token, ISimpleExpression left)
        {
            // Parse the comma-separated arguments until we hit, ")".
            var args = new List<ISimpleExpression>();

            // There may be no arguments at all.
            if (parser.IsMatch(TokenType.RIGHT_PAREN))
                return new FunctionCallExpression(left, args);
            do
            {
                args.Add(parser.ParseExpression(/*Precedence.ZERO*/));
            } while (parser.IsMatch(TokenType.COMMA));

            parser.Consume(TokenType.RIGHT_PAREN);

            return new FunctionCallExpression(left, args);
        }
    }
}