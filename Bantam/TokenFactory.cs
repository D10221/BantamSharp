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
            _punctuators = punctuators ?? throw new Exception("punctuators: Can't be null");
        }

        public IToken<TokenType> GetToken(ITokenSource input)
        {
            return GetPunctuator(input) ??
                 GetName(input) ??
                 GetNumber(input) ??
                 GetLiteral(input) ??
                 Token.Empty(default(TokenType), input);
        }

        private IToken<TokenType> GetPunctuator(ITokenSource source)
        {
            if (source == null || source.Value == null || !_punctuators.TryGetValue(source.Value, out var t))
            {
                return null;
            }
            return Token.From(t, source);
        }
        
        Regex _nameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z_0123456789]*$");
        private IToken<TokenType> GetName(ITokenSource source)
        {
            return source != null && _nameRegex.IsMatch(source.Value)
                ? Token.From(TokenType.NAME, source)
                : null;
        }
        Regex _numberRegex = new Regex(@"^\d+(\.)?(\d+)?$");
        private IToken<TokenType> GetNumber(ITokenSource input)
        {
            return input != null && _numberRegex.IsMatch(input.Value) ? Token.From(TokenType.NUMBER, input) : null;
        }
        Regex _literalRegex = new Regex("^('|\"|`).*('|\"|`)$");
        private IToken<TokenType> GetLiteral(ITokenSource input)
        {
            return input!= null && _literalRegex.IsMatch(input.Value) 
                    ? Token.From(TokenType.LITERAL, input)
                    : null;
        }

    }
}