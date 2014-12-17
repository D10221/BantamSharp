using System;
using System.Collections.Generic;
using SimpleParser.Parselets;

namespace SimpleParser
{
    public interface IParserConfig<TTokenType, TCHAR>
    {
        IDictionary<TTokenType, InfixParselet<TTokenType, TCHAR>> InfixParselets { get; }
        IDictionary<TTokenType, IPrefixParselet<TTokenType, TCHAR>> PrefixParselets { get; }
        void Register(Tuple<TTokenType, IPrefixParselet<TTokenType, TCHAR>> prefix);
        void Register(params Tuple<TTokenType, IPrefixParselet<TTokenType, TCHAR>>[] prefixes);
        void Register(Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>> infix);
        void Register(params Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>>[] infixes);
    }
}