using System.Collections;
using System.Collections.Generic;

namespace SimpleParser
{
    /// <summary>
    /// A very primitive lexer. Takes a string and splits it into a series of
    // Tokens. Operators and punctuation are mapped to unique keywords. Names,
    // which can be any series of letters, are turned into NAME tokens. All other
    // characters are ignored (except to separate names). Numbers and strings are
    // not supported. This is really just the bare minimum to give the parser
    // something to work with.
    /// </summary>
    // TTokenType is enum , 
    public interface ILexer<TTokenType,TCHAR>
    {
        /// <summary>
        /// Creates a new Lexer to tokenize the given string.
        /// @param text String to tokenize.
        /// </summary>
        IToken<TTokenType> Next();
        IEnumerable<TCHAR> InputText { get; }
    }
}