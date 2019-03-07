using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public class Parser<TTokenType> : IParser<TTokenType>
    {

        private readonly ILexer<TTokenType> _lexer;
        private readonly List<IToken<TTokenType>> _tokens = new List<IToken<TTokenType>>();
        private readonly IDictionary<TTokenType, IParselet<TTokenType>> _prefixParselets = new Dictionary<TTokenType, IParselet<TTokenType>>();
        private readonly IDictionary<TTokenType, InfixParselet<TTokenType>> _infixParselets = new Dictionary<TTokenType, InfixParselet<TTokenType>>();

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
            return GetInfixParselet(atoken.TokenType);
        }
        private IToken<TTokenType> LookAhead()
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
            var result = GetInfixParselet(tokenType);
            if (result != null)
                return result.Precedence;
            return 0;

            /* _parserMap.GetInfixParselet(tokenType)
                 .OnSuccess(x => precedence = x.Precedence);*/
        }
        private int GetPrecedence()
        {
            var token = LookAhead();
            return Equals(token.TokenType, default(TTokenType)) ? 0 : GetPrecedence(token.TokenType);
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
                    if (p != null) { left = p.Parse(this, atoken, left); }
                }
            }
            return left;
        }

        public bool IsMatch(TTokenType expected)
        {
            var token = LookAhead();
            if (!Equals(token.TokenType, expected))
            {
                return false;
            }

            Consume();
            return true;
        }

        public IToken<TTokenType> Consume(TTokenType expected)
        {
            var token = LookAhead();
            if (!Equals(token.TokenType, expected))
            {
                throw new ParseException("Expected token {0} and found {1}", expected, token.TokenType);
            }
            return Consume();
        }

        public IToken<TTokenType> Consume()
        {
            // Make sure we've read the token.
            LookAhead();
            var token = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }
        #endregion
    }
}