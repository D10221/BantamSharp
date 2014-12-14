using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser;

namespace BantamTests.Support
{
    public class FakeMap : IParserMap
    {
        private readonly IDictionary<TokenType, IPrefixParselet> _prefixParselets;
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets;

        public FakeMap(IDictionary<TokenType, IPrefixParselet> prefixParselets, IDictionary<TokenType, InfixParselet> infixParselets)
        {
            _prefixParselets = prefixParselets;
            _infixParselets = infixParselets;
        }

        public void Register(TokenType tokenType, IPrefixParselet parselet)
        {
            _prefixParselets.Add(tokenType,parselet);
        }

        public void Register(TokenType tokenType, InfixParselet parselet)
        {
           _infixParselets.Add(tokenType,parselet);
        }

        public Tuple<InfixParselet, bool> GetInfixParselet(TokenType tokenType)
        {
            var parselet = _infixParselets.FirstOrDefault(x=>x.Key == tokenType).Value;

            return new Tuple<InfixParselet, bool>(parselet,parselet!=null);
        }

        public Tuple<IPrefixParselet, bool> GetPrefixParselet(TokenType tokenType)
        {
            var parselet = _prefixParselets.FirstOrDefault(x => x.Key == tokenType).Value;

            return new Tuple<IPrefixParselet, bool>(parselet, parselet != null);
        }
    }


}