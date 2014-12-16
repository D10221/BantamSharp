using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Parselets;

namespace SimpleParser
{
    public class ParserMap<TTokenType,TCHAR> : IParserMap<TTokenType,TCHAR>
    {
        private readonly IDictionary<TTokenType, IPrefixParselet<TTokenType,TCHAR>> _prefixParselets;
        private readonly IDictionary<TTokenType, InfixParselet<TTokenType,TCHAR>> _infixParselets;

        public ParserMap(IDictionary<TTokenType, IPrefixParselet<TTokenType,TCHAR>> prefixParselets,
            IDictionary<TTokenType, InfixParselet<TTokenType,TCHAR>> infixParselets)
        {
            _prefixParselets = prefixParselets;
            _infixParselets = infixParselets;
        }

        public void Register(TTokenType tokenType, IPrefixParselet<TTokenType,TCHAR> parselet)
        {
            _prefixParselets.Add(tokenType,parselet);
        }

        public void Register(TTokenType tokenType, InfixParselet<TTokenType,TCHAR> parselet)
        {
           _infixParselets.Add(tokenType,parselet);
        }

        public Tuple<InfixParselet<TTokenType,TCHAR>, bool> GetInfixParselet(TTokenType tokenType)
        {
            var parselet = _infixParselets.FirstOrDefault(x=> Equals(x.Key, tokenType)).Value;

            return new Tuple<InfixParselet<TTokenType,TCHAR>, bool>(parselet,parselet!=null);
        }

        public Tuple<IPrefixParselet<TTokenType,TCHAR>, bool> GetPrefixParselet(TTokenType tokenType)
        {
            var parselet = _prefixParselets.FirstOrDefault(x => Equals(x.Key, tokenType)).Value;

            return new Tuple<IPrefixParselet<TTokenType,TCHAR>, bool>(parselet, parselet != null);
        }
    }


}