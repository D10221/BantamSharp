using System;
using System.Collections.Generic;
using System.Linq;
using ParseException = SimpleParser.ParseException<SimpleMaths.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<SimpleMaths.TokenType, string>;
using Prefix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>>;
using Infix = System.Tuple<SimpleMaths.TokenType, SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType, string>>;
using ParserConfig = SimpleParser.ParserConfig<SimpleMaths.TokenType, string>;
using ParserMap = SimpleParser.ParserMap<SimpleMaths.TokenType, string>;
using IParserMap = SimpleParser.IParserMap<SimpleMaths.TokenType, string>;
using Parser = SimpleParser.Parser<SimpleMaths.TokenType, string>;
using IBuilder = SimpleParser.IBuilder<string>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<string>;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType, string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType, string>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType, string>;

namespace SimpleMaths
{    
    public class ParserMap : IParserMap
    {
        private readonly IDictionary<TokenType,IPrefixParselet> _prefixParselets =  new Dictionary<TokenType, IPrefixParselet>();
        
        private readonly IDictionary<TokenType, InfixParselet> _infixParselets = new Dictionary<TokenType, InfixParselet>();

        public ParserMap(IEnumerable<Prefix> prefixes,IEnumerable<Infix> infixes)
        {
            _prefixParselets = prefixes.ToDictionary(x => x.Item1, x => x.Item2);

            _infixParselets = infixes.ToDictionary(x => x.Item1, x => x.Item2);            
        }

        public void Register(TokenType tokenType, IPrefixParselet parselet)
        {
            _prefixParselets.Add(tokenType, parselet);
        }

        public void Register(TokenType tokenType, InfixParselet parselet)
        {
            _infixParselets.Add(tokenType, parselet);
        }

        public Tuple<InfixParselet, bool> GetInfixParselet(TokenType tokenType)
        {
            InfixParselet parselet;
            var r = _infixParselets.TryGetValue(tokenType, out parselet);
            return new Tuple<InfixParselet, bool>(parselet, r);

        }

        public Tuple<IPrefixParselet, bool> GetPrefixParselet(TokenType tokenType)
        {
            IPrefixParselet parselet;
            var r = _prefixParselets.TryGetValue(tokenType, out parselet);
            return new Tuple<IPrefixParselet, bool>(parselet, r);

        }
    }

    
}