using System.Collections.Generic;

namespace SimpleParser
{
    public interface IBuilder
    {
        IBuilder Append(object cs);        
    }
}