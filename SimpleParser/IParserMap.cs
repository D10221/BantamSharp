using System;


namespace SimpleParser
{
    public interface IParserMap<TTokenType,TCHAR>
    {
        void Register(TTokenType tokenType, IParselet<TTokenType, TCHAR> parselet);
        void Register(TTokenType tokenType, InfixParselet<TTokenType, TCHAR> parselet);
        InfixParselet<TTokenType, TCHAR> GetInfixParselet(TTokenType tokenType);
        IParselet<TTokenType, TCHAR> GetPrefixParselet(TTokenType tokenType);
    }
}