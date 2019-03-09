using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleParser;

namespace Bantam
{
    public class TokenFactory : ITokenFactory<TokenType>
    {
        public static TokenFactory From(IDictionary<string, TokenType> punctuators)
        {
            return new TokenFactory(punctuators);
        }
        private readonly IDictionary<string, TokenType> _punctuators;

        public TokenFactory(IDictionary<string, TokenType> punctuators)
        {
            _punctuators = punctuators;
        }

        public IToken<TokenType> GetPunctuator(string c)
        {
            if (!_punctuators.TryGetValue(c, out var t))
            {
                return Token.Empty(default(TokenType));
            }
            return Token.From(t, c?.ToString());
        }
        Regex _nameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z_0123456789]*$");
        public IToken<TokenType> GetName(string input)
        {
            return input != null && _nameRegex.IsMatch(input) 
                ? new Token<TokenType>(TokenType.NAME, input) 
                : Token.Empty<TokenType>(default, input);
        }

    }
}