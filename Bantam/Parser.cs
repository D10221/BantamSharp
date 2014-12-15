using System.Collections.Generic;
using System.Linq;
using Bantam.Common;
using SimpleParser;
using PrefixResult = System.Tuple<SimpleParser.IPrefixParselet, bool>;
using InfixResult = System.Tuple<SimpleParser.InfixParselet, bool>;
namespace Bantam
{
    /// <summary>
    /// Bantam implementation of SimpleParser
    /// </summary>
    public class Parser : IParser
    {
        #region Dependencies

        private readonly ILexer _lexer;
        private readonly List<IToken> _tokens = new List<IToken>();

        #endregion


        public Parser(ILexer lexer,IParserMap parserMap)
        {
            _parserMap = parserMap;
            _lexer = lexer;
        }

        private PrefixResult GetPrefixParselet(TokenType tokenType)
        {
            return _parserMap.GetPrefixParselet(tokenType);
        }

        private InfixResult GetInfixParselet(IToken atoken)
        {
            return _parserMap.GetInfixParselet(atoken.TokenType);
        }

        #region IParser

        public ISimpleExpression ParseExpression(Precedence precedence = Precedence.ZERO)
        {

            var token = Consume();

            var prefix =
                GetPrefixParselet(token.TokenType)
                    .OnError(() => { throw new ParseException("No Prefix found for @name".Reglex("@name", token.GetText()), token); })
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


        public bool IsMatch(TokenType expected)
        {
            var token = lookAhead();
            if (token.TokenType != expected)
            {
                return false;
            }

            Consume();
            return true;
        }

        public IToken Consume(TokenType expected)
        {
            var token = lookAhead();
            if (token.TokenType != expected)
            {
                throw new ParseException("Expected token {0} and found {1}", expected, token.TokenType);
            }
            return Consume();
        }

        public IToken Consume()
        {
            // Make sure we've read the token.
            lookAhead();
            var token = _tokens.First();
            _tokens.RemoveAt(0);
            return token;
        }

        private IToken lookAhead()
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
            return tokenType == TokenType.NONE ? Precedence.ZERO : GetPrecedence(tokenType);
        }

        private Precedence GetPrecedence(TokenType tokenType)
        {
            var precedence = Precedence.ZERO ;
            var result = _parserMap.GetInfixParselet(tokenType);
            if (result.Ok())
                precedence = result.Parselet().Precedence;
            return precedence;

           /* _parserMap.GetInfixParselet(tokenType)
                .OnSuccess(x => precedence = x.Precedence);*/
        }

        private readonly IParserMap _parserMap;
    }
}