using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class ParserMap<TTokenType> : IParserMap<TTokenType>
    {
        private readonly IDictionary<TTokenType, IPrefixParselet<TTokenType>> _prefixParselets;
        private readonly IDictionary<TTokenType, InfixParselet<TTokenType>> _infixParselets;

        public ParserMap(IDictionary<TTokenType, IPrefixParselet<TTokenType>> prefixParselets,
            IDictionary<TTokenType, InfixParselet<TTokenType>> infixParselets)
        {
            _prefixParselets = prefixParselets;
            _infixParselets = infixParselets;
        }

        public void Register(TTokenType tokenType, IPrefixParselet<TTokenType> parselet)
        {
            _prefixParselets.Add(tokenType,parselet);
        }

        public void Register(TTokenType tokenType, InfixParselet<TTokenType> parselet)
        {
           _infixParselets.Add(tokenType,parselet);
        }

        public Tuple<InfixParselet<TTokenType>, bool> GetInfixParselet(TTokenType tokenType)
        {
            var parselet = _infixParselets.FirstOrDefault(x=> Equals(x.Key, tokenType)).Value;

            return new Tuple<InfixParselet<TTokenType>, bool>(parselet,parselet!=null);
        }

        public Tuple<IPrefixParselet<TTokenType>, bool> GetPrefixParselet(TTokenType tokenType)
        {
            var parselet = _prefixParselets.FirstOrDefault(x => Equals(x.Key, tokenType)).Value;

            return new Tuple<IPrefixParselet<TTokenType>, bool>(parselet, parselet != null);
        }
    }


}