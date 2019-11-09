



using System;
using System.Collections.Generic;

namespace uParserTests
{
    public class LiteralParselet : PrefixParselet
    {
        public TokenType TokenType { get; }

        public LiteralParselet(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public ISimpleExpression Parse(Func<int,ISimpleExpression> parse, IList<Token> lexer, Token token)
        {
            return new LiteralExpression(token);
        }

    }
}