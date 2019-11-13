using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TokenSplitting
{
    using static Util;
    ///<summary>
    ///
    ///</summary>
    public class Matcher
    {
        string[] delimiters;
        bool ignoreCase;
        Finder finder;
        ///<summary>
        /// returns match and match length for  char 'c' in input 'string'
        ///</summary>
        public Matcher(string[] delimiters, bool ignoreCase)
        {
            for(var i =0; i< delimiters.Length; i++){
                var slice = Exclude(delimiters, i);
                if(slice.Contains(delimiters[i])){
                    throw new Exception($"{delimiters[i]} specified more than once");
                }
            }
            this.delimiters = delimiters;
            this.ignoreCase = ignoreCase;
            finder = new Finder(ignoreCase);
        }
        
        static Regex _wordRegex = new Regex("^\\w+$");
        public (string match, int matchLength) IsMatch(string input, char c)
        {
            var match = string.Empty;
            var matchLength = 0;

            foreach (var symbol in delimiters)
            {
                var toFind = _wordRegex.IsMatch(symbol) ? $" {symbol} " : symbol;
                var found = finder.Find(input, c, toFind );
                if (Compare(toFind, found))
                {
                    match = symbol;
                    matchLength = toFind.Length;
                }
            }
            return (match, matchLength);
        }
        bool Compare(string a, string b)
        {
            return ignoreCase ? AreEqualIgnoreCase(a, b) : AreEqual(a, b);
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
}