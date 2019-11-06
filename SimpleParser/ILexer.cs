using System.Collections.Generic;

namespace SimpleParser
{
    public interface ILexer<T>
    {        
        T LookAhead();
        T Consume();        
    }
}