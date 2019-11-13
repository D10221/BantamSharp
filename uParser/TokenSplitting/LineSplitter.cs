using System.Collections.Generic;

namespace TokenSplitting
{
    using static Util;
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
        public IEnumerable<(string value, int column)> SplitLine(string line)
        {
            var result = new List<(string value, int column)>();
            var value = string.Empty; //buffer
            int columnIndex = 0;
            for (; columnIndex < line.Length;)
            {
                var inputChar = line[columnIndex];
                // find delimiter it might include Empty                
                var (match, matchLength) = matcher.IsMatch(
                    SliceToEnd(line, columnIndex),
                    inputChar
                );
                if (matchLength > 0)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        result.Add((value, columnIndex));
                        value = string.Empty;
                    }
                    columnIndex += matchLength; // skip match consummed chars length
                    result.Add((match,
                        columnIndex
                    ));
                }
                else if (char.IsWhiteSpace(inputChar) || inputChar == char.MinValue)
                {
                    // Check Whitespace anyway 
                    // empty temp buffer
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        result.Add((value, columnIndex == 0 ? 0 : columnIndex - 1));
                        value = string.Empty;
                    }
                    // if option 
                    if (includeEmpty)
                    {
                        result.Add((string.Empty, columnIndex));
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
                // may never looped
                var i = columnIndex == 0 ? 0 : columnIndex - 1;
                result.Add((value, i));
            }
            return result;
        }
    }
}