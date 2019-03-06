using System;
using System.Collections.Generic;
using System.Linq;


namespace SimpleParser
{
    public class ParserConfig<TTokenType, TCHAR> : IParserConfig<TTokenType, TCHAR>
    {
        private readonly List<Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>>> _INFIXES
            = new List<Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>>>();

        private readonly List<Tuple<TTokenType, IParselet<TTokenType, TCHAR>>> _PREFIXES
            = new List<Tuple<TTokenType, IParselet<TTokenType, TCHAR>>>();

        public IDictionary<TTokenType, InfixParselet<TTokenType, TCHAR>> InfixParselets
        {
            get { return _INFIXES.ToDictionary(p => p.Item1, p => p.Item2); }
        }

        public IDictionary<TTokenType, IParselet<TTokenType, TCHAR>> PrefixParselets
        {
            get { return _PREFIXES.ToDictionary(p => p.Item1, p => p.Item2); }
        }

        public void Register(Tuple<TTokenType, IParselet<TTokenType, TCHAR>> prefix)
        {
            _PREFIXES.Add(prefix);
        }

        public void Register(params Tuple<TTokenType, IParselet<TTokenType, TCHAR>>[] prefixes)
        {
            foreach (var prefix in prefixes)
            {
                Register(prefix);
            }
        }

        public void Register(Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>> infix)
        {
            _INFIXES.Add(infix);
        }

        public void Register(params Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>>[] infixes)
        {
            foreach (var infix in infixes)
            {
                Register(infix);
            }
        }
        public static readonly TestParserConfigFactory Factory = new TestParserConfigFactory();

        public class TestParserConfigFactory
        {
            public ParserConfig<TTokenType, TCHAR> CreateNew(
                IEnumerable<Tuple<TTokenType,
                IParselet<TTokenType, TCHAR>>> prefixes,
                IEnumerable<Tuple<TTokenType, InfixParselet<TTokenType, TCHAR>>> infixes)
            {
                var config = new ParserConfig<TTokenType, TCHAR>();
                if (prefixes != null) config.Register(prefixes.ToArray());
                if (infixes != null) config.Register(infixes.ToArray());
                return config;
            }
        }
    }
}