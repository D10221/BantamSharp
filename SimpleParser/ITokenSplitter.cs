using System.Collections.Generic;

namespace SimpleParser
{
    public interface ITokenSplitter
    {
        IEnumerable<ITokenSource> Split(string input);
    }
}