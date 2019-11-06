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
            return new Tokenizer<T>(punctuators, null);
        }
    }
    /// <summary>
    /// Convert text to tokens
    /// </summary>
    public class Tokenizer<TokenType>
    {
        private readonly IDictionary<string, TokenType> _punctuators;
        private readonly ITokenSplitter _tokenSplitter;

        public Tokenizer(IDictionary<TokenType, string> tokenTypes, TokenSplitter tokenSplitter)
        {
            _punctuators = tokenTypes.ToDictionary(x => x.Value, x => x.Key);
            _tokenSplitter = tokenSplitter ?? new TokenSplitter(
                delimiters: _punctuators.Select(x => x.Key)
            );
        }

        Func<ITokenSource, IToken<TokenType>> ToToken(ITokenFactory<TokenType> factory)
        {
            return x =>
            {
                var token = factory.GetToken(x);
                if (token == null || token.IsEmpty)
                {
                    throw new ParseException($"Unkown/Invalid Token:'{x}'");
                }
                return token;
            };
        }

        public IEnumerable<IToken<TokenType>> Tokenize(string text, ITokenFactory<TokenType> tokenFactory)
        {
            var toToken = ToToken(tokenFactory);
            return _tokenSplitter.Split(text).Select(toToken).ToArray();
        }

    }
}