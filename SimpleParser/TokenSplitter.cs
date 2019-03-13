using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleParser
{
    public class TokenSplitter : ITokenSplitter
    {
        private readonly string[] _delimiters;
        private readonly TokenSplitterOptions _options;

        public TokenSplitter(IEnumerable<string> delimiters, TokenSplitterOptions options = TokenSplitterOptions.None | TokenSplitterOptions.IgnoreCase)
        {
            if (delimiters == null)
            {
                throw new Exception($"param:'{nameof(delimiters)}' can't be null");
            }
            _delimiters = delimiters as string[] ?? delimiters.ToArray();

            if (_delimiters.Any(x => string.Equals(x, string.Empty)))
            {
                throw new Exception("Empty string isn not a valid delimiter"); ;
            }
            _options = options;
        }

        public IEnumerable<string> Split(string input)
        {
            var result = new List<string>();
            var xname = string.Empty;
            for (int inputIndex = 0; inputIndex < input.Length;)
            {
                var inputChar = input[inputIndex];
                var match = GetMatch(
                    Slice(input, inputIndex),
                    inputChar
                );
                if (match.Item2 > 0) // match.Item2 == Length
                {
                    if (!string.IsNullOrWhiteSpace(xname))
                    {
                        result.Add(xname);
                        xname = string.Empty;
                    }
                    inputIndex += match.Item2; // skip match consummed chars length
                    result.Add(match.Item1);
                    continue;
                }
                else
                {
                    if (char.IsWhiteSpace(inputChar) || inputChar == char.MinValue)
                    {
                        if (!string.IsNullOrWhiteSpace(xname))
                        {
                            result.Add(xname);
                            xname = string.Empty;
                        }
                        if (_options.HasFlag(TokenSplitterOptions.IncludeEmpty))
                        {
                            result.Add(string.Empty);
                        }

                        ++inputIndex;
                        continue;
                    }
                    xname += inputChar;
                    ++inputIndex;
                }
            }
            if (!string.IsNullOrWhiteSpace(xname))
            {
                result.Add(xname);
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
