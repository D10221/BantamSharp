using System;
using System.Collections.Generic;
using System.Linq;

namespace TokenSplitting
{
    ///<summary>
    ///
    ///</summary>
    public class TokenSplitter
    {
        string[] delimiters;
        bool ignoreCase = false;
        bool includeEmpty = false;

        private readonly LineSplitter lineSplitter;
        public TokenSplitter(
            string[] delimiters,
            bool ignoreCase = false,
            bool includeEmpty = false
        )
        {
            var matcher = new Matcher(delimiters, ignoreCase);
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
                var columns = lineSplitter.SplitLine(line);
                result.AddRange(columns.Select(x =>
                {
                    var (value, column) = x;
                    return (TokenSource)(value, line: lineIndex, column);
                }));
            }
            return result;
        }
    }
}