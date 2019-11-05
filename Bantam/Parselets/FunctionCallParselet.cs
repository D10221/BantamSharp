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
            var next = parser.LookAhead();
            if (next.TokenType == TokenType.PARENT_RIGHT)
            {
                parser.Consume();
                return new FunctionCallExpression(left, args);
            }
            do
            {
                var e = parser.Parse((int)Bantam.Precedence.ZERO);
                args.Add(e);                
                next = parser.LookAhead();
                if(next.TokenType == TokenType.COMMA){
                    parser.Consume();
                }
    
            } while (next.TokenType == TokenType.COMMA);

            if (next.TokenType != TokenType.PARENT_RIGHT)
            {
                throw new ParseException($"Expected {TokenType.PARENT_RIGHT}");
            }            
            parser.Consume();
            return new FunctionCallExpression(left, args);
        }
    }
}