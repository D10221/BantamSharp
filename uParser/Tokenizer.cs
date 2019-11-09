using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using TokenSplitting;

namespace uParser
{
    /// <summary>
    /// Convert text to tokens
    /// </summary>
    public class Tokenizer
    {
        ///<summary>
        ///
        ///</summary>
        public static Func<string, Token[]> Tokenize = CreateTokenizer(Punctuators.Reverse);
        ///<summary>
        ///
        ///</summary>
        public static Func<string, Token[]> CreateTokenizer(
            IDictionary<string, TokenType> punctuators,
            bool ignoreCase = false,
            bool includeEmpty = false
            )
        {
            var tokenFactory = new TokenFactory(punctuators);
            var delimiters = punctuators.Select(x => x.Key).ToArray();
            var splitter = new TokenSplitter(delimiters, ignoreCase, includeEmpty);
            return text => splitter.Split(text).Select(tokenFactory.GetToken).ToArray();
        }
        ///<summary>
        /// Glue between TokenSource and Token
        ///</summary>
        class TokenFactory
        {
            IDictionary<string, TokenType> punctuators;
            public TokenFactory(IDictionary<string, TokenType> punctuators)
            {
                this.punctuators = punctuators;
            }
            public Token GetToken(TokenSource input)
            {
                return GetPunctuator(punctuators, input) ??
                     GetName(input) ??
                     GetNumber(input) ??
                     GetLiteral(input) ??
                     new Token(default(TokenType), input);
            }

            static Token GetPunctuator(IDictionary<string, TokenType> punctuators, TokenSource source)
            {
                if (source == null || source.Value == null || !punctuators.TryGetValue(source.Value, out var t))
                {
                    return null;
                }
                return new Token(t, source);
            }

            static Regex _nameRegex = new Regex(@"^[a-zA-Z_][a-zA-Z_0123456789]*$");
            private static Token GetName(TokenSource source)
            {
                return source != null && _nameRegex.IsMatch(source.Value)
                    ? new Token(TokenType.NAME, source)
                    : null;
            }
            static Regex _numberRegex = new Regex(@"^\d+(\.)?(\d+)?$");
            static private Token GetNumber(TokenSource input)
            {
                return input != null && _numberRegex.IsMatch(input.Value) ? new Token(TokenType.NUMBER, input) : null;
            }
            static Regex _literalRegex = new Regex("^('|\"|`).*('|\"|`)$");
            private static Token GetLiteral(TokenSource input)
            {
                return input != null && _literalRegex.IsMatch(input.Value)
                        ? new Token(TokenType.LITERAL, input)
                        : null;
            }
        }
    }
}