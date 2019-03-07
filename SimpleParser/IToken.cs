
using System;

namespace SimpleParser
{
    //TOkenType is the current enum 
    public interface IToken<TTokenType> where TTokenType: Enum
    {
        TTokenType TokenType { get; }
        string Text { get; }

        string ToString();
        bool HasValue { get; }
        bool IsEmpty { get; }
    }
}