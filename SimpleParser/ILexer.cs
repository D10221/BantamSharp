using System;

namespace SimpleParser
{
    public interface ILexer<TTokenType> 
    {
        IToken<TTokenType> Next();        
    }
}