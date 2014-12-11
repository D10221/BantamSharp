﻿/**
 * A simple token class. These are generated by Lexer and consumed by Parser.
 */

using System.Globalization;

namespace SimpleParser
{
    public class Token : IToken
    {
        public Token(TokenType type, string text)
        {
            mType = type;
            mText = text;
        }

        public TokenType GetTokenType()
        {
            return mType;
        }

        public string GetText()
        {
            return mText;
        }

        public override string ToString()
        {
            return mText;
        }

        private readonly TokenType mType;
        
        private readonly string mText;

        public static Token New(TokenType tokenType, string text)
        {
            return new Token(tokenType, text);
        }

        public static IToken New(TokenType tokenType, char c)
        {
            return New(tokenType, c.ToString(CultureInfo.InvariantCulture));
        }

        public static Token Empty()
        {
            return New(TokenType.NONE, null);
        }

        public bool HasValue
        {
            get { return this.GetTokenType() != TokenType.NONE; }
        }
        
        public bool IsEmpty
        {
            get { return this.GetTokenType() == TokenType.NONE; }
        }
    }
}