using System;
using System.Collections.Generic;

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
    }
}