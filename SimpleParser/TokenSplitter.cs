﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleParser
{
    public class TokenSplitter : ITokenSplitter
    {
        private readonly string[] _delimiters;
        private readonly TokenSplitterOptions _options;

        public string[] EOL { get; } = new[] { "\n", "\n\r", "\r\n", "\r" };

        public TokenSplitter(IEnumerable<string> delimiters, TokenSplitterOptions options = TokenSplitterOptions.None | TokenSplitterOptions.IgnoreCase)
        {
            if (delimiters == null)
            {
                throw new ArgumentException($"param:'{nameof(delimiters)}' can't be null");
            }
            _delimiters = (delimiters as string[] ?? delimiters.ToArray());

            if (_delimiters.Any(x => string.Equals(x, string.Empty)))
            {
                throw new ArgumentException("Empty string is not a valid delimiter"); ;
            }
            _options = options;
        }


        public IEnumerable<ITokenSource> Split(string input)
        {
            var result = new List<ITokenSource>();

            string[] array = input.Split(EOL, StringSplitOptions.None);
            for (int lineIndex = 0; lineIndex < array.Length; lineIndex++)
            {
                string line = array[lineIndex];
                var xname = string.Empty;
                int columnIndex = 0;
                for (; columnIndex < line.Length;)
                {
                    var inputChar = line[columnIndex];
                    var match = GetMatch(
                        Slice(line, columnIndex),
                        inputChar
                    );
                    if (match.Item2 > 0) // match.Item2 == Length
                    {
                        if (!string.IsNullOrWhiteSpace(xname))
                        {
                            result.Add(
                                TokenSource.From(xname, lineIndex, columnIndex)
                            );
                            xname = string.Empty;
                        }
                        columnIndex += match.Item2; // skip match consummed chars length
                        result.Add(TokenSource.From(
                            match.Item1,
                            lineIndex,
                            columnIndex
                        ));
                        continue;
                    }
                    else
                    {
                        if (char.IsWhiteSpace(inputChar) || inputChar == char.MinValue)
                        {
                            if (!string.IsNullOrWhiteSpace(xname))
                            {
                                result.Add(
                                    TokenSource.From(xname, lineIndex, columnIndex)
                                );
                                xname = string.Empty;
                            }
                            if (_options.HasFlag(TokenSplitterOptions.IncludeEmpty))
                            {
                                result.Add(
                                    TokenSource.From(string.Empty, lineIndex, columnIndex)
                                );
                            }

                            ++columnIndex;
                            continue;
                        }
                        xname += inputChar;
                        ++columnIndex;
                    }
                }
                if (!string.IsNullOrWhiteSpace(xname))
                {
                    result.Add(
                        TokenSource.From(xname, lineIndex, columnIndex)
                            );
                }
            }
            return result;
        }

        private static string Slice(string input, int start)
        {
            return input.Substring(start, input.Length - start);
        }
        Regex _wordRegex = new Regex("^\\w+$");
        ///<summary>
        /// return match & consummed chars
        ///</summary>
        private Tuple<string, int> GetMatch(string sub, char c)
        {
            var match = string.Empty;
            var matchLength = 0;

            foreach (var symbol in _delimiters)
            {
                var toFind = _wordRegex.IsMatch(symbol) ? $" {symbol} " : symbol;
                var found = Find(c, toFind, sub);
                if (AreEquals(toFind, found))
                {
                    match = symbol;
                    matchLength = toFind.Length;
                }
            }
            return Tuple.Create(match, matchLength);
        }

        private bool AreEquals(string toFind, string found)
        {
            return string.Equals(
                toFind,
                found,
                _options.HasFlag(
                    TokenSplitterOptions.IgnoreCase)
                        ? StringComparison.OrdinalIgnoreCase
                        : StringComparison.Ordinal);
        }

        /// <summary>
        /// Find longest macth
        /// </summary>        
        private string Find(char c, string symbol, string sub)
        {
            var result = string.Empty;
            for (var symbolLength = 0; symbolLength < symbol.Length; symbolLength++)
            {
                char symbolChar = symbol[symbolLength];
                var next = symbolLength == 0 ? c : PeekAt(sub, symbolLength);
                var ok = AreEqual(symbolChar, next);
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

        private bool AreEqual(char a, char b)
        {
            if (_options.HasFlag(TokenSplitterOptions.IgnoreCase))
                return char.ToUpperInvariant(a).Equals(char.ToUpperInvariant(b));
            return char.Equals(a, b);
        }

        private char PeekAt(string input, int index)
        {
            return (input?.Length ?? 0) > index ? input[index] : default;
        }
    }
}
