using System.Collections.Generic;

namespace SimpleParser
{
    public interface IBuilder
    {
        IBuilder Append(string cs);
        IBuilder Append(object cs);
        string Build();
    }
}