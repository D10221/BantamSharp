
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    /// <summary>
    /// Bantam implementation of SimpleParser
    /// </summary>
    public class Parser<TTokenType, TCHAR> : IParser<TTokenType>
    {
        #region Dependencies

        private readonly ILexer<TTokenType> _lexer;
        private readonly List<IToken<TTokenType>> _tokens = new List<IToken<TTokenType>>();

        private readonly IDictionary<TTokenType, IParselet<TTokenType>> _prefixParselets = new Dictionary<TTokenType, IParselet<TTokenType>>();

        private readonly IDictionary<TTokenType, InfixParselet<TTokenType>> _infixParselets = new Dictionary<TTokenType, InfixParselet<TTokenType>>();

        #endregion
        public Parser(
            ILexer<TTokenType> lexer,
            IDictionary<TTokenType, IParselet<TTokenType>> prefixParselets,
            IDictionary<TTokenType, InfixParselet<TTokenType>> infixParselets)
        {
            _lexer = lexer;
            _prefixParselets = prefixParselets;
            _infixParselets = infixParselets;
        }

        private IParselet<TTokenType> GetPrefixParselet(TTokenType tokenType)
        {
            _prefixParselets.TryGetValue(tokenType, out var value);
            return value;
        }

        private InfixParselet<TTokenType> GetInfixParselet(TTokenType tokenType)
        {
            _infixParselets.TryGetValue(tokenType, out var value);
            return value;
        }

        private InfixParselet<TTokenType> GetInfixParselet(IToken<TTokenType> atoken)
        {
            _infixParselets.TryGetValue(atoken.TokenType, out var value);
            return value;
        }

        #region IParser

        public ISimpleExpression ParseExpression(int precedence = 0)
        {
            var token = Consume();

            var prefix =
                GetPrefixParselet(token.TokenType);
            var left = prefix?.Parse(this, token); //Expression
            while (precedence < GetPrecedence())
            {
                var atoken = Consume();
                if (atoken.HasValue)
                {
                    var p = GetInfixParselet(atoken);
                    if (p != null) { left = p.Parse(this, left, atoken); }
                }
            }
            return left;
        }


        public bool IsMatch(TTokenType expected)
        {
            var token = lookAhead();
            if (!Equals(token.TokenType, expected))
            {
                return false;
            }

            Consume();
            return true;
        }

        public IToken<TTokenType> Consume(TTokenType expected)
        {
            var token = lookAhead();
            if (!Equals(token.TokenType, expected))
            {
                throw new ParseException<TTokenType>("Expected token {0} and found {1}", expected, token.TokenType);
            }
            return Consume();
        }

        public IToken<TTokenType> Consume()
        {
            // Make sure we've read the token.
            lookAhead();
            var token = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }

        private IToken<TTokenType> lookAhead()
        {
            while (!_tokens.Any())
            {
                var token = _lexer.Next();
                _tokens.Add(token);
            }
            return _tokens.First();
        }

        #endregion

        private int GetPrecedence()
        {
            var token = lookAhead();
            var tokenType = token.TokenType;
            return Equals(tokenType, default(TTokenType)) ? 0 : GetPrecedence(tokenType);
        }

        private int GetPrecedence(TTokenType tokenType)
        {
            var result = GetInfixParselet(tokenType);
            if (result != null)
                return result.Precedence;
            return 0;

            /* _parserMap.GetInfixParselet(tokenType)
                 .OnSuccess(x => precedence = x.Precedence);*/
        }
    }
}