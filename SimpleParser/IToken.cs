
using System;

namespace SimpleParser
{
    //TOkenType is the current enum 
    public interface IToken<TTokenType> 
    {
        TTokenType TokenType { get; }                
        bool HasValue { get; }
        bool IsEmpty { get; }
    }
}