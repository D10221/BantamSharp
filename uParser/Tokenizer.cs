using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            var getToken = TokenFactory.GetToken(punctuators);
            var delimiters = punctuators.Select(x => x.Key).ToArray();
            var splitter = TokenSplitter.Splitter(delimiters, ignoreCase, includeEmpty);
            return text => splitter(text).Select(getToken).ToArray();
        }
        ///<summary>
        ///
        ///</summary>
        static class TokenFactory
        {
            static public Func<TokenSource, Token> GetToken(IDictionary<string, TokenType> punctuators)
            {
                return input => GetPunctuator(punctuators, input) ??
                     GetName(input) ??
                     GetNumber(input) ??
                     GetLiteral(input) ??
                     new Token(default(TokenType), input);
            }

            private static Token GetPunctuator(IDictionary<string, TokenType> punctuators, TokenSource source)
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
        ///<summary>
        ///
        ///</summary>
        class TokenSplitter
        {
            static readonly string[] EOL = new[] { "\n\r", "\n", "\r" };
            public static Func<string, IEnumerable<TokenSource>> Splitter(
                string[] delimiters,
                bool ignoreCase = false,
                bool includeEmpty = false)
            {
                var matcher = new Matcher(delimiters, ignoreCase);
                var lineSplitter = new LineSplitter(matcher, includeEmpty);

                return input =>
                {
                    var result = new List<TokenSource>();

                    string[] array = input.Split(EOL, StringSplitOptions.None);
                    for (int lineIndex = 0; lineIndex < array.Length; lineIndex++)
                    {
                        string line = array[lineIndex];
                        result.AddRange(
                            lineSplitter.SplitLine(line, lineIndex)
                        );
                    }
                    return result;
                };
            }
            ///<summary>
            ///
            ///</summary>
            class LineSplitter
            {
                bool includeEmpty;
                Matcher matcher;
                public LineSplitter(Matcher matcher, bool includeEmpty)
                {
                    this.matcher = matcher;
                    this.includeEmpty = includeEmpty;
                }
                public IEnumerable<TokenSource> SplitLine(
                string line,
                int lineIndex
                )
                {
                    var result = new List<TokenSource>();
                    var value = string.Empty;
                    int columnIndex = 0;
                    for (; columnIndex < line.Length;)
                    {
                        var inputChar = line[columnIndex];
                        // find delimiter it might include Empty                
                        var (match, matchLength) = matcher.IsMatch(
                            Slice(line, columnIndex),
                            inputChar
                        );
                        if (matchLength > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                result.Add(
                                    TokenSource.From(value, lineIndex, columnIndex)
                                );
                                value = string.Empty;
                            }
                            columnIndex += matchLength; // skip match consummed chars length
                            result.Add(
                                TokenSource.From(
                                match,
                                lineIndex,
                                columnIndex
                            ));
                        }
                        else if (char.IsWhiteSpace(inputChar) || inputChar == char.MinValue)
                        {
                            // Check WHite Space anyway 
                            // empty temp buffer
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                result.Add(
                                    TokenSource.From(value, lineIndex, columnIndex == 0 ? 0 : columnIndex - 1)
                                );
                                value = string.Empty;
                            }
                            // if option 
                            if (includeEmpty)
                            {
                                result.Add(
                                    TokenSource.From(string.Empty, lineIndex, columnIndex)
                                );
                            }
                            ++columnIndex;
                        }
                        else
                        {
                            // process 'value'
                            value += inputChar;
                            ++columnIndex;
                        }
                    }
                    //empty buffer
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        result.Add(
                            TokenSource.From(
                            value,
                            lineIndex,
                            columnIndex == 0 ? 0 : columnIndex - 1 //may never looped
                        ));
                    }
                    return result;
                }
                private static string Slice(string input, int start)
                {
                    return input.Substring(start, input.Length - start);
                }
            }
            ///<summary>
            ///
            ///</summary>
            class Matcher
            {
                static Regex _wordRegex = new Regex("^\\w+$");
                string[] delimiters;
                bool ignoreCase;
                ///<summary>
                /// return match & consummed chars
                ///</summary>
                public Matcher(string[] delimiters, bool ignoreCase)
                {
                    this.delimiters = delimiters;
                    this.ignoreCase = ignoreCase;
                }
                bool Compare(string a, string b)
                {
                    return ignoreCase ? AreEqualIgnoreCase(a, b) : AreEqual(a, b);
                }
                public (string match, int matchLength) IsMatch(string sub, char c)
                {
                    var match = string.Empty;
                    var matchLength = 0;

                    foreach (var symbol in delimiters)
                    {
                        var toFind = _wordRegex.IsMatch(symbol) ? $" {symbol} " : symbol;
                        var found = new Finder(ignoreCase).Find(c, toFind, sub);
                        if (Compare(toFind, found))
                        {
                            match = symbol;
                            matchLength = toFind.Length;
                        }
                    }
                    return (match, matchLength);
                }
                private static bool AreEqual(string toFind, string found)
                {
                    return string.Equals(toFind, found, StringComparison.Ordinal);
                }
                private static bool AreEqualIgnoreCase(string toFind, string found)
                {
                    return string.Equals(toFind, found, StringComparison.OrdinalIgnoreCase);
                }
            }
            ///<summary>
            ///
            ///</summary>
            class Finder
            {
                bool ignoreCase;
                public Finder(bool ignoreCase)
                {
                    this.ignoreCase = ignoreCase;
                }
                private static bool AreEqual(char a, char b) => char.Equals(a, b);
                private static bool AreEqualIgnoreCase(char a, char b) => char.ToUpperInvariant(a).Equals(char.ToUpperInvariant(b));

                private bool Compare(char a, char b)
                {
                    return ignoreCase ? AreEqualIgnoreCase(a, b) : AreEqual(a, b);
                }
                /// <summary>
                /// Find longest match
                /// </summary>        
                public string Find(char c, string symbol, string sub)
                {
                    var result = string.Empty;
                    for (var symbolLength = 0; symbolLength < symbol.Length; symbolLength++)
                    {
                        char symbolChar = symbol[symbolLength];
                        var next = symbolLength == 0 ? c : PeekAt(sub, symbolLength);
                        var ok = Compare(symbolChar, next);
                        if (ok)
                        {
                            result += next;
                        }
                        else
                        {
                            break;
                        }
                    }
                    return result;
                }
                private static char PeekAt(string input, int index)
                {
                    return (input?.Length ?? 0) > index ? input[index] : default;
                }
            }
        }

    }
}