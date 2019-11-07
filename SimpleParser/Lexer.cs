using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class Lexer
    {
        public static Lexer<IToken<T>> From<T>(IEnumerable<IToken<T>> tokens)
        {
            return new Lexer<IToken<T>>(tokens);
        }
    }
    public class Lexer<T> : ILexer<T>
    {
        private readonly IList<T> _tokens;
        private readonly IEnumerator<T> _enumerator;
        public Lexer(IEnumerable<T> tokens)
        {           
            _tokens = tokens.ToList();
            _enumerator = tokens.GetEnumerator();
        }
        public T LookAhead()
        {
            while (!_tokens.Any())
            {
                var token = Next();
                // Done ? 
                if (Equals(token, default)) return token;
                // don't buffer  null
                _tokens.Add(token);
            }
            return _tokens.First();
        }
        public T Consume()
        {
            LookAhead();
            var token = _tokens.FirstOrDefault();
            if (!Equals(token, default)) _tokens.RemoveAt(0);
            return token;
        }
        public T Next()
        {
            if (_enumerator.MoveNext())
                return _enumerator.Current;
            return default;
        }
        public override string ToString()
        {
            return "[" +_tokens.Select(a => $"'{a}'").Aggregate((a, b) => a + "," + b) + "]";
        }
    }
}