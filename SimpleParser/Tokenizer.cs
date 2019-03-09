using SimpleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleParser
{
    public static class Tokenizer
    {
        public static Tokenizer<T> From<T>(IDictionary<T, string> punctuators)
        {
            return new Tokenizer<T>(punctuators);
        }
    }
    /// <summary>
    /// Convert text to tokens
    /// </summary>
    public class Tokenizer<TokenType>
    {
        private readonly IDictionary<string, TokenType> _punctuators;

        public Tokenizer(IDictionary<TokenType, string> tokenTypes)
        {
            _punctuators = tokenTypes.ToDictionary(x => x.Value, x => x.Key);
        }

        Func<string, IToken<TokenType>> ToToken(ITokenFactory<TokenType> factory)
        {
            return x =>
            {
                var token = factory.GetPunctuator(x);
                if (!token.IsEmpty)
                {
                    return token;
                }
                token = factory.GetName(x);
                if (!token.IsEmpty)
                {
                    return token;
                }
                throw new TokenizerException($"Invalid Token:'{x}'");
            };
        }

        public IEnumerable<IToken<TokenType>> Tokenize(string text, ITokenFactory<TokenType> tokenFactory)
        {
            var toToken = ToToken(tokenFactory);
            var delimiters = _punctuators.Select(x => x.Key).ToArray();
            return Split(text, delimiters).Select(toToken).ToArray();
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
    }
}