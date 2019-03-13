using System.Collections.Generic;

namespace SimpleParser
{
    public interface ILexer<TTokenType>
    {
        IEnumerable<IToken<TTokenType>> Tokens {get;}
        IToken<TTokenType> Next();
    }
}