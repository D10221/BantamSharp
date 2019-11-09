using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace uParser
{
    public enum TokenSplitterOptions
    {
        None = 0,
        IncludeEmpty = 2,
        IgnoreCase = 4,
    }
    public class TokenSplitter
    {
        private readonly string[] _delimiters;
        private readonly TokenSplitterOptions _options;

        public string[] EOL { get; } = new[] {
            "\n\r",
            "\n",
            "\r"
        };

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


        public IEnumerable<TokenSource> Split(string input)
        {
            var result = new List<TokenSource>();

            string[] array = input.Split(EOL, StringSplitOptions.None);
            for (int lineIndex = 0; lineIndex < array.Length; lineIndex++)
            {
                string line = array[lineIndex];
                result.AddRange(
                    SplitLine(line, lineIndex)
                );
            }
            return result;
        }
        IEnumerable<TokenSource> SplitLine(string line, int lineIndex)
        {
            var result = new List<TokenSource>();
            var value = string.Empty;
            int columnIndex = 0;
            for (; columnIndex < line.Length;)
            {
                var inputChar = line[columnIndex];
                // find delimiter it might include Empty
                var match = GetMatch(
                    Slice(line, columnIndex),
                    inputChar
                );
                if (match.Item2 > 0)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        result.Add(
                            TokenSource.From(value, lineIndex, columnIndex)
                        );
                        value = string.Empty;
                    }
                    columnIndex += match.Item2; // skip match consummed chars length
                    result.Add(
                        TokenSource.From(
                        match.Item1,
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
                    if (_options.HasFlag(TokenSplitterOptions.IncludeEmpty))
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