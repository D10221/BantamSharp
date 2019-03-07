using System.Collections.Generic;

namespace SimpleParser
{
    public interface ILexer<TTokenType>
    {
        IToken<TTokenType> Next();
        string InputText { get; }
    }
}