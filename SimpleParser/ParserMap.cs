
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class ParserMap<TTokenType, TCHAR> : IParserMap<TTokenType, TCHAR>
    {
        private readonly IDictionary<TTokenType, IParselet<TTokenType, TCHAR>> _prefixParselets;
        private readonly IDictionary<TTokenType, InfixParselet<TTokenType, TCHAR>> _infixParselets;

        public ParserMap(IDictionary<TTokenType, IParselet<TTokenType, TCHAR>> prefixParselets,
            IDictionary<TTokenType, InfixParselet<TTokenType, TCHAR>> infixParselets)
        {
            _prefixParselets = prefixParselets;
            _infixParselets = infixParselets;
        }

        public void Register(TTokenType tokenType, IParselet<TTokenType, TCHAR> parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        public void Register(TTokenType tokenType, InfixParselet<TTokenType, TCHAR> parselet)
        {
            _infixParselets.Add(tokenType, parselet);
        }

        public InfixParselet<TTokenType, TCHAR> GetInfixParselet(TTokenType tokenType)
        {
            return _infixParselets.FirstOrDefault(x => Equals(x.Key, tokenType)).Value;
        }

        public IParselet<TTokenType, TCHAR> GetPrefixParselet(TTokenType tokenType)
        {
            return _prefixParselets.FirstOrDefault(x => Equals(x.Key, tokenType)).Value;
        }
    }
}