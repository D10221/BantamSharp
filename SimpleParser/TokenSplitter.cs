using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class TokenSplitter : ITokenSplitter
    {
        string[] _delimiters;
        TokenSplitterOptions options;

        public TokenSplitter(IEnumerable<string> delimiters, TokenSplitterOptions options = TokenSplitterOptions.None)
        {
            this._delimiters = delimiters as string[] ?? delimiters.ToArray();
            this.options = options;
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
                if (match.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(xname))
                    {
                        result.Add(xname);
                        xname = string.Empty;
                    }
                    inputIndex += match.Length;
                    result.Add(match);
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
                        if (this.options.HasFlag(TokenSplitterOptions.IncludeEmpty))
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

        private string GetMatch(string sub, char c)
        {
            var ret = string.Empty;

            foreach (var symbol in _delimiters)
            {
                var found = Find(c, symbol, sub);
                if (found.Length > ret.Length)
                {
                    ret = found;
                }
            }
            return ret;
        }

        private string Find(char c, string symbol, string sub)
        {
            var result = string.Empty;
            for (var symbolLength = 0; symbolLength < symbol.Length; symbolLength++)
            {
                char symbolChar = symbol[symbolLength];
                var next = symbolLength == 0 ? c : PeekAt(sub, symbolLength);
                var ok = symbolChar.Equals(next);
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

        private char PeekAt(string input, int index)
        {
            return (input?.Length ?? 0) > index ? input[index] : default(char);
        }
    }
}
