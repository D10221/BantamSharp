using System;
using System.Collections.Generic;


namespace SimpleParser
{
    public interface IParserConfig<TTokenType, TCHAR>
    {
        IDictionary<TTokenType, InfixParselet<TTokenType, TCHAR>> InfixParselets { get; }
        IDictionary<TTokenType, IParselet<TTokenType, TCHAR>> PrefixParselets { get; }
        void Register(Tuple<TTokenType, IParselet<TTokenType, TCHAR>> prefix);
        void Register(params Tuple<TTokenType, IParselet<TTokenType, TCHAR>>[] prefixes);
        void Register(Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>> infix);
        void Register(params Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>>[] infixes);
    }
}