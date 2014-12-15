using System;
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

        private readonly ILexer<TTokenType,TCHAR> _lexer;
        private readonly List<IToken<TTokenType>> _tokens = new List<IToken<TTokenType>>();

        #endregion

        public Parser(ILexer<TTokenType,TCHAR> lexer,IParserMap<TTokenType> parserMap)
        {
            _parserMap = parserMap;
            _lexer = lexer;
        }

        private Tuple<IPrefixParselet<TTokenType>, bool> GetPrefixParselet(TTokenType tokenType)
        {
            return _parserMap.GetPrefixParselet(tokenType);
        }

        private Tuple<InfixParselet<TTokenType>, bool> GetInfixParselet(IToken<TTokenType> atoken)
        {
            return _parserMap.GetInfixParselet(atoken.TokenType);
        }

        #region IParser

        public ISimpleExpression ParseExpression(Precedence precedence = Precedence.ZERO)
        {

            var token = Consume();

            var prefix =
                GetPrefixParselet(token.TokenType)
                    .OnError(() => {
                        throw new ParseException<TTokenType>("No Prefix found for @name"
                        .Replace("@name", token.GetText()), token); 
                    })
                    .OkResult();

            var left = prefix.Parse(this, token); //Expression

            while (precedence < GetPrecedence())
            {
                var atoken = Consume();
                GetInfixParselet(atoken)
                    .OnSuccess(parselet =>
                    {
                        left = parselet.Parse(this, left, atoken);
                    });
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

        private Precedence GetPrecedence() {
           
            var token = lookAhead();
            var tokenType = token.TokenType;            
            return Equals(tokenType, default(TTokenType)) ? Precedence.ZERO : GetPrecedence(tokenType);
        }

        private Precedence GetPrecedence(TTokenType tokenType)
        {
            var precedence = Precedence.ZERO ;
            var result = _parserMap.GetInfixParselet(tokenType);
            if (result.Ok())
                precedence = result.Parselet().Precedence;
            return precedence;

           /* _parserMap.GetInfixParselet(tokenType)
                .OnSuccess(x => precedence = x.Precedence);*/
        }

        private readonly IParserMap <TTokenType>_parserMap;
    }
}