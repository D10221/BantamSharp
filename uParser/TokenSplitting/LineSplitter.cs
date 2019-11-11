using System.Collections.Generic;

namespace TokenSplitting
{
    ///<summary>
    ///
    ///</summary>
    public class LineSplitter
    {
        bool includeEmpty;
        Matcher matcher;
        public LineSplitter(Matcher matcher, bool includeEmpty)
        {
            this.matcher = matcher;
            this.includeEmpty = includeEmpty;
        }
        public IEnumerable<TokenSource> SplitLine(string line, int lineIndex)
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
                    // Check Whitespace anyway 
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
}