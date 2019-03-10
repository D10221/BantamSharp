using System.Collections.Generic;

namespace SimpleParser
{
    public interface ITokenSplitter
    {
        IEnumerable<string> Split(string input);
    }
}