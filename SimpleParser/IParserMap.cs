using System;
using SimpleParser.Parselets;

namespace SimpleParser
{
    public interface IParserMap<TTokenType,TCHAR>
    {
        void Register(TTokenType tokenType, IPrefixParselet<TTokenType, TCHAR> parselet);
        void Register(TTokenType tokenType, InfixParselet<TTokenType, TCHAR> parselet);
        Tuple<InfixParselet<TTokenType, TCHAR>, bool> GetInfixParselet(TTokenType tokenType);
        Tuple<IPrefixParselet<TTokenType, TCHAR>, bool> GetPrefixParselet(TTokenType tokenType);
    }
}