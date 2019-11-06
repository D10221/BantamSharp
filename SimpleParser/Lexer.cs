using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class Lexer
    {
        public static Lexer<T> From<T>(IEnumerable<IToken<T>> tokens)
        {
            return new Lexer<T>(tokens);
        }
    }

    public class Lexer<T> : ILexer<T>
    {
        private readonly IEnumerable<IToken<T>> _tokens;

        private readonly IEnumerator<IToken<T>> _enumerator;

        public Lexer(IEnumerable<IToken<T>> tokens)
        {
            _tokens = tokens;
            _enumerator = tokens.GetEnumerator();
        }

        public IToken<T> Next()
        {
            if (_enumerator.MoveNext())
                return _enumerator.Current;
            return Token.Empty<T>();
        }

        public override string ToString()
        {
            return _tokens.Select(a => $"'{a}'").Aggregate((a, b) => a + ";" + b);
        }
    }
}