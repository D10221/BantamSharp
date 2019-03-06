using System.Collections.Generic;
using System.Linq;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.InfixParselet<SimpleMaths.TokenType, string>>;
using InfixParselet = SimpleParser.InfixParselet<SimpleMaths.TokenType, string>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType, string>;
using IParselet = SimpleParser.IParselet<SimpleMaths.TokenType, string>;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.IParselet<SimpleMaths.TokenType, string>>;

namespace SimpleMaths
{
    public class ParserMap : IParserMap
    {
        private readonly IDictionary<TokenType, IParselet> _prefixParselets = new Dictionary<TokenType, IParselet>();

        private readonly IDictionary<TokenType, InfixParselet> _infixParselets = new Dictionary<TokenType, InfixParselet>();

        public ParserMap(IEnumerable<Prefix> prefixes, IEnumerable<Infix> infixes)
        {
            _prefixParselets = prefixes.ToDictionary(x => x.Item1, x => x.Item2);

            _infixParselets = infixes.ToDictionary(x => x.Item1, x => x.Item2);
        }

        public void Register(TokenType tokenType, IParselet parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        public void Register(TokenType tokenType, InfixParselet parselet)
        {
            _infixParselets.Add(tokenType, parselet);
        }

        public InfixParselet GetInfixParselet(TokenType tokenType)
        {
            _infixParselets.TryGetValue(tokenType, out InfixParselet parselet);
            return parselet;

        }

        public IParselet GetPrefixParselet(TokenType tokenType)
        {
            _prefixParselets.TryGetValue(tokenType, out IParselet parselet);
            return parselet;

        }
    }


}