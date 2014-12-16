using System;
using System.Collections.Generic;

namespace SimpleParser
{
    public interface ITokenConfig<TTokenType, TCHAR>
    {
        /// <summary>
        /// If the TokenType represents a punctuator (i.e. a token that can split an identifier like '+', this will get its text.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        TCHAR Punctuator(TTokenType tokenType);

        bool IsValidPunctuator( TCHAR c);

        IEnumerable<Tuple<TTokenType, TCHAR>> TokenTypes { get; }
        
    }
}