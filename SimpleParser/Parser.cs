
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    /// <summary>
    /// Bantam implementation of SimpleParser
    /// </summary>
    public class Parser<TTokenType, TCHAR> : IParser<TTokenType, TCHAR>
    {
        #region Dependencies

        private readonly ILexer<TTokenType, TCHAR> _lexer;
        private readonly List<IToken<TTokenType>> _tokens = new List<IToken<TTokenType>>();

        #endregion

        public Parser(ILexer<TTokenType, TCHAR> lexer, IParserMap<TTokenType, TCHAR> parserMap)
        {
            _parserMap = parserMap;
            _lexer = lexer;
        }

        private IParselet<TTokenType, TCHAR> GetPrefixParselet(TTokenType tokenType)
        {
            return _parserMap.GetPrefixParselet(tokenType);
        }

        private InfixParselet<TTokenType, TCHAR> GetInfixParselet(IToken<TTokenType> atoken)
        {
            return _parserMap.GetInfixParselet(atoken.TokenType);
        }

        #region IParser

        public ISimpleExpression<TCHAR> ParseExpression(int precedence = 0)
        {
            var token = Consume();

            var prefix =
                GetPrefixParselet(token.TokenType);
            if (prefix == null) throw new ParseException<TTokenType>("No Prefix found for @name"
                         .Replace("@name", token.GetText()), token);

            var left = prefix.Parse(this, token); //Expression
            while (precedence < GetPrecedence())
            {
                var atoken = Consume();
                var p = GetInfixParselet(atoken);
                if (p != null) { left = p.Parse(this, left, atoken); }
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
            var result = _parserMap.GetInfixParselet(tokenType);
            if (result != null)
                return result.Precedence;
            return 0;

            /* _parserMap.GetInfixParselet(tokenType)
                 .OnSuccess(x => precedence = x.Precedence);*/
        }

        private readonly IParserMap<TTokenType, TCHAR> _parserMap;
    }
}