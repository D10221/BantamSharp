using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class Parser<TTokenType> : IParser<TTokenType>
    {
        private readonly ILexer<TTokenType> _lexer;
        private readonly IList<IToken<TTokenType>> _tokens = new List<IToken<TTokenType>>();
        private readonly IEnumerable<IParselet<TTokenType>> _parselets;

        public Parser(
            ILexer<TTokenType> lexer,
            IEnumerable<IParselet<TTokenType>> parselets)
        {
            _lexer = lexer;
            _parselets = parselets;
        }

        private IParselet<TTokenType> GetParselet(TTokenType tokenType, ParseletType parseletType)
        {
            return _parselets.FirstOrDefault(x => x.ParseletType == parseletType && x.TokenType.Equals(tokenType));
        }

        public IToken<TTokenType> LookAhead()
        {
            while (!_tokens.Any())
            {
                var token = _lexer.Next();
                _tokens.Add(token);
            }
            return _tokens.First();
        }
        private int GetPrecedence(TTokenType tokenType)
        {
            var result = GetParselet(tokenType, ParseletType.Infix);
            if (result != null)
                return result.Precedence;
            return 0;
        }
        private int NextPrecedence()
        {
            var token = LookAhead();
            return Equals(token.TokenType, default(TTokenType)) ? 0 : GetPrecedence(token.TokenType);
        }

        #region IParser

        public ISimpleExpression<TTokenType> Parse(int precedence = 0)
        {
            var token = Consume();

            var parselet =
                    GetParselet(token.TokenType, ParseletType.Prefix)
                    ?? GetParselet(token.TokenType, ParseletType.Infix);

            var left = parselet?.Parse(this, token, null);

            while (precedence < NextPrecedence()) //Get Next Precedence
            {
                var atoken = Consume();
                if (!atoken.IsEmpty)
                {
                    var p = GetParselet(atoken.TokenType, ParseletType.Infix);
                    if (p != null)
                    {
                        left = p.Parse(this, atoken, left);
                    }
                }
            }
            return left ?? new EmptyExpression<TTokenType>();
        }       
        public IToken<TTokenType> Consume()
        {
            LookAhead();
            var token = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }
        #endregion
    }
}