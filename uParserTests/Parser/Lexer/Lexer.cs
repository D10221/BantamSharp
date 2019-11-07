using System.Collections.Generic;
using System.Linq;

namespace uParserTests
{
    public class Lexer
    {
        private List<Token> queue;
        private readonly IEnumerator<Token> _enumerator;
        public Lexer(IEnumerable<Token> tokens)
        {
            queue = new List<Token>();
            _enumerator = tokens.GetEnumerator();
        }
        public Token LookAhead()
        {
            var (token, any) = MaybeNext();
            if (any)
            {
                queue = queue.Append(token).ToList();                
            }
            return any ? token : queue.FirstOrDefault();
        }
        public Token Consume()
        {
            LookAhead();
            var token = queue.FirstOrDefault();
            if (queue.Any()) queue.RemoveAt(0);
            return token;
        }
        public void Next()
        {
            if (!queue.Any())
            {
                var (next, any) = MaybeNext();
                if (any)
                {
                    queue.Add(next);
                }
            }
        }
        (Token, bool) MaybeNext()
        {
            var any = _enumerator.MoveNext();
            return (any ? _enumerator.Current : null, any);
        }
        public override string ToString()
        {
            return "[" + queue.Select(a => $"'{a}'").Aggregate((a, b) => a + "," + b) + "]";
        }
    }
}