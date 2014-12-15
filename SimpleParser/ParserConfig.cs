using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class ParserConfig<TTokenType>
    {
        private readonly List<Tuple<TTokenType, InfixParselet<TTokenType>>> INFIXES = new List<Tuple<TTokenType, InfixParselet<TTokenType>>>();

        private readonly List<Tuple<TTokenType, IPrefixParselet<TTokenType>>> PREFIXES = new List<Tuple<TTokenType, IPrefixParselet<TTokenType>>>();

        public IDictionary<TTokenType, InfixParselet<TTokenType>> InfixParselets
        {
            get { return INFIXES.ToDictionary(p => p.Item1, p => p.Item2); }
        }

        public IDictionary<TTokenType, IPrefixParselet<TTokenType>> PrefixParselets
        {
            get { return PREFIXES.ToDictionary(p => p.Item1, p => p.Item2); }
        }

        public void Register(Tuple<TTokenType, IPrefixParselet<TTokenType>> prefix)
        {
            PREFIXES.Add(prefix);
        }

        public void Register(params Tuple<TTokenType, IPrefixParselet<TTokenType>>[] prefixes)
        {
            foreach (var prefix in prefixes)
            {
                Register(prefix);
            }
        }

        public void Register(Tuple<TTokenType, InfixParselet<TTokenType>> infix)
        {
            INFIXES.Add(infix);
        }

        public void Register(params Tuple<TTokenType, InfixParselet<TTokenType>>[] infixes)
        {
            foreach (var infix in infixes)
            {
                Register(infix);
            }
        }
        public static readonly TestParserConfigFactory Factory = new TestParserConfigFactory();

        public class TestParserConfigFactory
        {
            public ParserConfig<TTokenType> CreateNew(IEnumerable<Tuple<TTokenType,
                IPrefixParselet<TTokenType>>> prefixes, IEnumerable<Tuple<TTokenType, InfixParselet<TTokenType>>> infixes)
            {
                var config = new ParserConfig<TTokenType>();
                if (prefixes != null) config.Register(prefixes.ToArray());
                if (infixes != null) config.Register(infixes.ToArray());
                return config;
            }
        }
    }
}