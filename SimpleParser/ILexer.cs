using System;

namespace SimpleParser
{
    public interface ILexer<TTokenType> where TTokenType : Enum
    {
        IToken<TTokenType> Next();
        string Text { get; }
    }
}