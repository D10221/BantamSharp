namespace TokenSplitting
{
    ///<summary>
    ///
    ///</summary>
    public class Finder
    {
        bool ignoreCase;
        public Finder(bool ignoreCase)
        {
            this.ignoreCase = ignoreCase;
        }        
        /// <summary>
        /// Find longest match
        /// </summary>        
        public string Find(string input, char c, string symbol)
        {
            var result = string.Empty;
            for (var symbolLength = 0; symbolLength < symbol.Length; symbolLength++)
            {
                char symbolChar = symbol[symbolLength];
                var next = symbolLength == 0 ? c : PeekAt(input, symbolLength);                
                if (!Compare(symbolChar, next)) break;                
                result += next;                
            }
            return result;
        }
        private static char PeekAt(string input, int index)
        {
            return (input?.Length ?? 0) > index ? input[index] : default;
        }
        private bool Compare(char a, char b)
        {
            return ignoreCase ? AreEqualIgnoreCase(a, b) : AreEqual(a, b);
        }
        private static bool AreEqual(char a, char b) => char.Equals(a, b);
        private static bool AreEqualIgnoreCase(char a, char b) => char.ToUpperInvariant(a).Equals(char.ToUpperInvariant(b));
    }
}