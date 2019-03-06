using System;


namespace SimpleParser
{
    public interface IParserMap<TTokenType,TCHAR>
    {       
        InfixParselet<TTokenType, TCHAR> GetInfixParselet(TTokenType tokenType);
        IParselet<TTokenType, TCHAR> GetPrefixParselet(TTokenType tokenType);
    }
}