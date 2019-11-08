﻿



namespace uParserTests
{
    public class LiteralParselet : PrefixParselet
    {
        public TokenType TokenType { get; }

        public LiteralParselet(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public ISimpleExpression Parse(Parser parser, Lexer lexer, Token token)
        {
            return new LiteralExpression(token);
        }

    }
}