﻿using Bantam.Expressions;

using ISimpleExpression = SimpleParser.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using SimpleParser;

namespace Bantam.Paselets
{
    /// <summary>
    /// Simple parselet for a named variable like "abc"
    /// </summary>
    public class NameParselet : IParselet<TokenType, char>
    {
        public ISimpleExpression Parse(IParser parser, IToken token)
        {
            return new NameExpression(token.GetText());
        }
    }
}