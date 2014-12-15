using System;

namespace SimpleParser
{
    public interface IParserMap<TTokenType>
    {
        void Register(TTokenType tokenType, IPrefixParselet<TTokenType> parselet);
        void Register(TTokenType tokenType, InfixParselet<TTokenType> parselet);
        Tuple<InfixParselet<TTokenType>, bool> GetInfixParselet(TTokenType tokenType);
        Tuple<IPrefixParselet<TTokenType>, bool> GetPrefixParselet(TTokenType tokenType);
    }
}