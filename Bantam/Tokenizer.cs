using SimpleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bantam
{
    public static class TokenizerExtensions
    {
        public static IEnumerable<IToken<TokenType>> Tokenize(this IDictionary<TokenType, string> punctuators, string text)
        {
            return new Tokenizer(punctuators).Tokenize(text);
        }
    }
    /// <summary>
    /// Convert text to tokens
    /// </summary>
    internal class Tokenizer
    {
        private readonly IDictionary<string, TokenType> _punctuators;

        public Tokenizer(IDictionary<TokenType, string> tokenTypes)
        {
            _punctuators = tokenTypes.ToDictionary(x => x.Value, x => x.Key);
        }

        public IEnumerable<IToken<TokenType>> Tokenize(string text)
        {
            return Split(text, _punctuators.Select(x => x.Key).ToArray()).Select(ToToken).ToArray();
        }

        public static IEnumerable<string> Split(string input, params object[] delimiters)
        {
            var results = new List<string>();
            var token = string.Empty; ;
            foreach (var c in input.Trim())
            {
                var isBlank = char.IsWhiteSpace(c) || char.MinValue.Equals(c);
                var found = delimiters.FirstOrDefault(x =>
                {
                    return x.ToString().Equals(c.ToString());
                })?.ToString();
                if (found == null)
                {
                    if (!isBlank)
                    {
                        token += c;
                    }
                }
                var isFound = found != null;
                if (isBlank || isFound)
                {
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        results.Add(token);
                    }
                    if (isFound)
                    {
                        results.Add(found);
                    }
                    token = string.Empty;
                }
            }
            if (!string.IsNullOrWhiteSpace(token))
            {
                results.Add(token);
            }
            return results;
        }

        private IToken<TokenType> ToToken(string x)
        {
            var token = TryGetPunctuator(x);
            if (!token.IsEmpty)
            {
                return token;
            }
            token = TryGetLetter(x);
            if (!token.IsEmpty)
            {
                return token;
            }
            throw new Exception($"Invalid Token:'{x}'");
        }

        private IToken<TokenType> TryGetPunctuator(string c)
        {
            if (!_punctuators.TryGetValue(c, out var t))
            {
                return Token.Empty(default(TokenType));
            }
            return Token.From(t, c.ToString());
        }

        private IToken<TokenType> TryGetLetter(string c)
        {
            var input = c.ToString();
            return LooksLikeLetter(input) ? new Token<TokenType>(TokenType.NAME, input) : Token.Empty<TokenType>();
        }

        private static bool LooksLikeLetter(string input)
        {
            var regex = new Regex(@"\w");
            bool isMatch = regex.IsMatch(input);
            return isMatch;
        }
    }
}