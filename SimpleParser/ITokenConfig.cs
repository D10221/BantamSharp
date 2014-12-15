using System;
using System.Collections.Generic;

namespace SimpleParser
{
    public interface ITokenConfig<T>
    {
        /// <summary>
        /// If the TokenType represents a punctuator (i.e. a token that can split an identifier like '+', this will get its text.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        char Punctuator( TokenType tokenType);

        bool IsValidPunctuator( T c);
        IEnumerable<Tuple<TokenType, T>> TokenTypes { get; }
    }
}