using System;

namespace SimpleParser
{
    public interface IParserMap
    {
        void Register(TokenType tokenType, IPrefixParselet parselet);
        void Register(TokenType tokenType, InfixParselet parselet);
        Tuple<InfixParselet, bool> GetInfixParselet(TokenType tokenType);
        Tuple<IPrefixParselet, bool> GetPrefixParselet(TokenType tokenType);
    }
}