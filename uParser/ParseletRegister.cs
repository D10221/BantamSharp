﻿using System;
using System.Collections.Generic;

namespace uParser
{
    using static Parselets;
    public static class ParseletRegister
    {
        public static void AddPostfix(
            this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Add(token, PostfixOperatorParselet((int)precedence));
        }
        public static void AddPrefix(this IDictionary<TokenType, PrefixParselet> prefixParselets,
            TokenType token, Precedence precedence)
        {
            prefixParselets.Add(token, PrefixOperatorParselet((int)precedence));
        }
        public static void AddInfixLeft(this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Add(token, BinaryOperatorParselet((int)precedence, isRight: false));
        }
        public static void AddInfixRight(this IDictionary<TokenType, (int, InfixParselet)> infixParselets,
            TokenType token, Precedence precedence)
        {
            infixParselets.Add(token, BinaryOperatorParselet((int)precedence, isRight: true));
        }
    }
}