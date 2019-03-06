using System.Collections.Generic;

namespace SimpleParser
{
    public interface ILexer<TTokenType, TCHAR>
    {
        IToken<TTokenType> Next();
        IEnumerable<TCHAR> InputText { get; }
    }
}