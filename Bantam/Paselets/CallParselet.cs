﻿using System.Collections.Generic;
using Bantam.Expressions;
using SimpleParser;

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
                return new CallSimpleExpression(left, args);
            do
            {
                args.Add(parser.ParseExpression());
            } while (parser.IsMatch(TokenType.COMMA));

            parser.Consume(TokenType.RIGHT_PAREN);

            return new CallSimpleExpression(left, args);
        }

        public int GetPrecedence()
        {
            return (int) Precedence.CALL;
        }
    }
}
