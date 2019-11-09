using System;
using System.Collections.Generic;

namespace uParserTests
{
    using static Parselets;
    public static class ParseletRegister
    {
        public static void Register(this IDictionary<TokenType, PrefixParselet> prefixParselets,
            TokenType token, PrefixParselet parselet)
        {
            prefixParselets.Add(token, parselet);
        }
        public static void Register(this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, (int, InfixParselet) parselet)
        {
            infixParselets.Add(token, parselet);
        }
        public static void Postfix(
            this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Register(token, PostfixOperatorParselet((int)precedence));
        }
        public static void Prefix(this IDictionary<TokenType, PrefixParselet> prefixParselets,
            TokenType token, Precedence precedence)
        {
            prefixParselets.Register(token, PrefixOperatorParselet((int)precedence));
        }

        public static void InfixLeft(this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Register(token, BinaryOperatorParselet((int)precedence, false));
        }

        public static void InfixRight(this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Register(token, BinaryOperatorParselet((int)precedence, true));
        }
    }
}