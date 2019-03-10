using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleParser;

namespace Bantam
{
    public class TokenFactory : ITokenFactory<TokenType>
    {
        public static TokenFactory From(IDictionary<string, TokenType> punctuators)
        {
            return new TokenFactory(punctuators ?? new Dictionary<string, TokenType>());
        }
        private readonly IDictionary<string, TokenType> _punctuators;

        public TokenFactory(IDictionary<string, TokenType> punctuators)
        {
            if (punctuators == null) throw new Exception("punctuators: Can't be null");
            _punctuators = punctuators;
        }

        public IToken<TokenType> GetToken(string input)
        {
            // if(input == null) throw new Exception($"input can't be null");
            return GetPunctuator(input) ??
                 GetName(input) ??
                 GetNumber(input) ??
                 Token.Empty(default(TokenType), input);
        }
        private IToken<TokenType> GetPunctuator(string c)
        {
            if (c == null || !_punctuators.TryGetValue(c, out var t))
            {
                return null;
            }
            return Token.From(t, c?.ToString());
        }
        Regex _nameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z_0123456789]*$");
        private IToken<TokenType> GetName(string input)
        {
            return input != null && _nameRegex.IsMatch(input)
                ? Token.From(TokenType.NAME, input)
                : null;
        }
        Regex _numberRegex = new Regex(@"^\d+(\.)?(\d+)?$");
        private IToken<TokenType> GetNumber(string input)
        {
            return input != null && _numberRegex.IsMatch(input) ? Token.From(TokenType.NUMBER, input) : null;
        }

    }
}