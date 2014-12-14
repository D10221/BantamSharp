using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser;

namespace BantamTests.Support
{
    public class TestParserConfig
    {
        private  readonly List<Tuple<TokenType, InfixParselet>> INFIXES = new List<Tuple<TokenType, InfixParselet>>();

        private  readonly List<Tuple<TokenType, IPrefixParselet>> PREFIXES = new List<Tuple<TokenType, IPrefixParselet>>();

        public IDictionary<TokenType, InfixParselet> InfixParselets
        {
            get { return INFIXES.ToDictionary(p => p.Item1, p => p.Item2); }
        }

        public IDictionary<TokenType, IPrefixParselet> PrefixParselets
        {
            get { return PREFIXES.ToDictionary(p => p.Item1, p => p.Item2); }
        }

        public void Register(Tuple<TokenType, IPrefixParselet> prefix)
        {
            PREFIXES.Add(prefix);
        }

        public void Register(params Tuple<TokenType, IPrefixParselet>[] prefixes)
        {
            foreach (var prefix in prefixes)
            {
                Register(prefix);
            }
        }

        public void Register(Tuple<TokenType, InfixParselet> infix)
        {
            INFIXES.Add(infix);
        }

        public void Register(params Tuple<TokenType, InfixParselet>[] infixes)
        {
            foreach (var infix in infixes)
            {
                Register(infix);
            }
        }
        public static readonly TestParserConfigFactory Factory = new TestParserConfigFactory();

        public class TestParserConfigFactory
        {
            public  TestParserConfig CreateNew(IEnumerable<Tuple<TokenType, IPrefixParselet>> prefixes, IEnumerable<Tuple<TokenType, InfixParselet>> infixes)
            {
                var config = new TestParserConfig();
                if (prefixes != null) config.Register(prefixes.ToArray());
                if (infixes != null) config.Register(infixes.ToArray());
                return config;
            }
        }
    }
}