using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace tokenSplitter
{
    ///<summary>
    ///
    ///</summary>
    public class TokenSplitter
    {
        string[] delimiters;
        bool ignoreCase = false;
        bool includeEmpty = false;
        private readonly Matcher matcher;
        private readonly LineSplitter lineSplitter;
        public TokenSplitter(
            string[] delimiters,
            bool ignoreCase = false,
            bool includeEmpty = false
        )
        {
            matcher = new Matcher(delimiters, ignoreCase);
            lineSplitter = new LineSplitter(matcher, includeEmpty);
        }
        static readonly string[] EOL = new[] { "\n\r", "\n", "\r" };
        public IList<TokenSource> Split(string input)
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
            /// return match and consummed chars
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