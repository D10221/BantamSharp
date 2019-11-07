using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace uParserTests
{    
    public class TokenFactory
    {
       
        private readonly IDictionary<string, TokenType> _punctuators;

        public TokenFactory(IDictionary<string, TokenType> punctuators)
        {
            _punctuators = punctuators ?? throw new Exception("punctuators: Can't be null");
        }

        public Token GetToken(TokenSource input)
        {
            return GetPunctuator(input) ??
                 GetName(input) ??
                 GetNumber(input) ??
                 GetLiteral(input) ??
                 new Token(default(TokenType), input);
        }

        private Token GetPunctuator(TokenSource source)
        {
            if (source == null || source.Value == null || !_punctuators.TryGetValue(source.Value, out var t))
            {
                return null;
            }
            return new Token(t, source);
        }

        Regex _nameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z_0123456789]*$");
        private Token GetName(TokenSource source)
        {
            return source != null && _nameRegex.IsMatch(source.Value)
                ? new Token(TokenType.NAME, source)
                : null;
        }
        Regex _numberRegex = new Regex(@"^\d+(\.)?(\d+)?$");
        private Token GetNumber(TokenSource input)
        {
            return input != null && _numberRegex.IsMatch(input.Value) ? new Token(TokenType.NUMBER, input) : null;
        }
        Regex _literalRegex = new Regex("^('|\"|`).*('|\"|`)$");
        private Token GetLiteral(TokenSource input)
        {
            return input != null && _literalRegex.IsMatch(input.Value)
                    ? new Token(TokenType.LITERAL, input)
                    : null;
        }

    }
}